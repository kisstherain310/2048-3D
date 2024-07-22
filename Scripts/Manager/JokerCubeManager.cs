using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JokerCubeManager : MonoBehaviour
{
    private ListCube listCube;
    private Transform defaultCubeSpawnPoint;
    [HideInInspector] public JokerCube jokerCube = null;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }
    // ----------------- SpawnJokerCube -----------------
    public void InitJokerCube(){
        JokerCube newJokerCube = ObjectPooler.Instance.SpawnFromPool("JokerCube", defaultCubeSpawnPoint.position, Quaternion.identity).GetComponent<JokerCube>();   
        newJokerCube.SetMainCube(true);
        newJokerCube.SetActiveLine(true);
        jokerCube = newJokerCube;
    }

    // ----------------- Helper Method -----------------
    public void SpawnJokerCube()
    {
        if(jokerCube != null) DestroyJokerCube(jokerCube);
        foreach (Cube cube in listCube.cubes)
        {
            if (cube.isMainCube) {
                GameManager.Instance.classicCubeManager.DestroyCube(cube);
                break;
            }
        }
        InitJokerCube();
    }

    public void DestroyJokerCube(JokerCube jokerCube)
    {
        ObjectPooler.Instance.ReturnToPool("JokerCube", jokerCube.gameObject);
        jokerCube = null;
    }
}
