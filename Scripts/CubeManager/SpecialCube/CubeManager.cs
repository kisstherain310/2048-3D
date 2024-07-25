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
        GameManager.Instance.listCube.AddDataCube(newCube.GetComponent<BaseCube>());
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
    public void SpawnCube(string tag , Vector3 position, Quaternion rotation, bool isMainCube)
    {
        T newCube = ObjectPooler.Instance.SpawnFromPool(tag, position, rotation).GetComponent<T>();
        newCube.GetComponent<BaseCube>().SetMainCube(isMainCube);
        if(isMainCube) newCube.GetComponent<BaseCube>().SetActiveLine(true);
        else newCube.GetComponent<BaseCube>().SetActiveLine(false);
        GameManager.Instance.listCube.AddDataCube(newCube.GetComponent<BaseCube>());
    }

    public void DestroyCube(T cube)
    {
        ObjectPooler.Instance.ReturnToPool(cube.GetComponent<BaseCube>().poolTag, cube.gameObject);
        GameManager.Instance.listCube.RemoveDataCube(cube.GetComponent<BaseCube>());
    }
}
