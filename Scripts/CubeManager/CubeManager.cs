using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CubeManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected ListCube listCube;
    protected Transform defaultCubeSpawnPoint;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }

    // ----------------- SpawnCube -----------------
    public void InitCube(string poolTag)
    {
        T newCube = ObjectPooler.Instance.SpawnFromPool(poolTag, defaultCubeSpawnPoint.position, Quaternion.identity).GetComponent<T>();
        newCube.GetComponent<BaseCube>().SetMainCube(true);
        newCube.GetComponent<BaseCube>().SetActiveLine(true);
        newCube.GetComponent<BaseCube>().initEffect.growEffect();
        GameManager.Instance.mainCube = newCube.GetComponent<BaseCube>();
    }

    // ----------------- Helper Method -----------------
    public void SpawnCube(string poolTag)
    {
        if (GameManager.Instance.mainCube != null) { // Xóa mainCube cũ ở vị trí xuất phát 
            GameManager.Instance.DestroyMainCube();
        }
        foreach (Cube cube in listCube.cubes)
        {
            if (cube.isMainCube)
            {
                GameManager.Instance.classicCubeManager.DestroyCube(cube);
                break;
            }
        }
        InitCube(poolTag);
    }

    public void DestroyCube(T cube)
    {
        ObjectPooler.Instance.ReturnToPool(cube.GetComponent<BaseCube>().poolTag, cube.gameObject);
    }
}
