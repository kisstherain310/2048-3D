using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform defaultCubeSpawnPoint;
    [SerializeField] public ClassicCubeManager classicCubeManager;
    [SerializeField] public JokerCubeManager jokerCubeManager;
    [SerializeField] private ListCube listCube;

    private void Awake()
    {
        Instance = this;
    }
    // ----------------- Init -----------------
    private void Start()
    {
        InitClassicCubeManager();
        InitJokerCubeManager();
    }
    private void InitClassicCubeManager()
    {
        classicCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
        classicCubeManager.InitClassicCube();
    }
    private void InitJokerCubeManager()
    {
        jokerCubeManager.Initialize(listCube, defaultCubeSpawnPoint);
    }

    // ----------------- Helper Method -----------------
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
