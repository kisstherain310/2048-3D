using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JokerCubeManager : MonoBehaviour
{
    private ListCube listCube;
    private Transform defaultCubeSpawnPoint;
    public JokerCube jokerCube = null;

    public void getListCube(ListCube listCube)
    {
        this.listCube = listCube;
    }
    public void getDefaultCubeSpawnPoint(Transform defaultCubeSpawnPoint)
    {
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }
    public void InitJokerCube(){
        JokerCube newJokerCube = ObjectPooler.Instance.SpawnFromPool("JokerCube", defaultCubeSpawnPoint.position, Quaternion.identity).GetComponent<JokerCube>();   
        newJokerCube.SetMainCube(true);
        newJokerCube.SetActiveLine(true);
        jokerCube = newJokerCube;
    }

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
