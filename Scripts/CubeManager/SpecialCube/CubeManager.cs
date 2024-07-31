using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CubeManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected ListCube listCube;
    public Transform defaultCubeSpawnPoint;
    private GameObject cubeParent;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint, GameObject cubeParent)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
        this.cubeParent = cubeParent;
    }
    public void SetParent(GameObject cubeParent)
    {
        this.cubeParent = cubeParent;
    }

    // ----------------- SpawnCube -----------------
    public void InitCube(string poolTag)
    {
        T newCube = ObjectPooler.Instance.SpawnFromPool(poolTag, defaultCubeSpawnPoint.position, Quaternion.identity).GetComponent<T>();
        newCube.GetComponent<BaseCube>().SetMainCube(true);
        newCube.GetComponent<BaseCube>().SetActiveLine(true);
        newCube.GetComponent<BaseCube>().OffTrail();
        newCube.GetComponent<BaseCube>().initEffect.growEffect();
        newCube.transform.SetParent(cubeParent.transform);
        GameManager.Instance.mainCube = newCube.GetComponent<BaseCube>();
        GameManager.Instance.listCube.AddDataCube(newCube.GetComponent<BaseCube>());
    }

    // ----------------- Helper Method -----------------
    public void CheckNull(){
        if (GameManager.Instance.mainCube != null) { // Xóa mainCube cũ ở vị trí xuất phát 
            GameManager.Instance.DestroyMainCube();
        }
    }
    public void SpawnCube(string poolTag)
    {
        CheckNull(); // sinh ra cube mới ở vị trí xuất phát
        InitCube(poolTag);
        GameManager.Instance.dataManager.Save();
    }
    public void SpawnCube(string tag , Vector3 position, Quaternion rotation, bool isMainCube)
    {
        T newCube = ObjectPooler.Instance.SpawnFromPool(tag, position, rotation).GetComponent<T>();
        newCube.GetComponent<BaseCube>().SetMainCube(isMainCube);
        if(isMainCube){
            newCube.GetComponent<BaseCube>().SetActiveLine(true);
            GameManager.Instance.mainCube = newCube.GetComponent<BaseCube>();
        }
        else newCube.GetComponent<BaseCube>().SetActiveLine(false);
        newCube.GetComponent<BaseCube>().OffTrail();
        newCube.transform.SetParent(cubeParent.transform);
        GameManager.Instance.listCube.AddDataCube(newCube.GetComponent<BaseCube>());
        GameManager.Instance.dataManager.Save();
    }

    public void DestroyCube(T cube)
    {
        cube.transform.SetParent(null);
        ObjectPooler.Instance.ReturnToPool(cube.GetComponent<BaseCube>().poolTag, cube.gameObject);
        GameManager.Instance.listCube.RemoveDataCube(cube.GetComponent<BaseCube>());
    }
}
