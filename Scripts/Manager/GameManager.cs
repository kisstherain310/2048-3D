using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform defaultCubeSpawnPoint;
    [SerializeField] public ClassicCubeManager classicCubeManager;
    [SerializeField] public JokerCubeManager jokerCubeManager;
    [SerializeField] private ListCube listCube;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitClassicCubeManager();
        InitJokerCubeManager();
    }
    private void InitClassicCubeManager()
    {
        classicCubeManager.getDefaultCubeSpawnPoint(defaultCubeSpawnPoint);
        classicCubeManager.getListCube(listCube);
        classicCubeManager.InitClassicCube();
    }
    private void InitJokerCubeManager()
    {
        jokerCubeManager.getDefaultCubeSpawnPoint(defaultCubeSpawnPoint);
        jokerCubeManager.getListCube(listCube);
    }

    public Vector3 FindCubeNearest(Cube newCube)
    {
        float minDistance = Mathf.Infinity;
        Vector3 nearestCubePosition = Vector3.zero;
        foreach (Cube cube in listCube.cubes)
        {
            if (cube.isMainCube) continue;
            if(cube.cubeNumber == newCube.cubeNumber){
                float distance = Vector3.Distance(cube.transform.position, newCube.transform.position);
                if (distance < minDistance && distance > 0)
                {
                    minDistance = distance;
                    nearestCubePosition = cube.transform.position;
                }
            }
        }
        return nearestCubePosition;
    }
}
