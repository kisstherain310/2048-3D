using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicCubeManager : MonoBehaviour
{
    private ListCube listCube;
    public Transform defaultCubeSpawnPoint;
    public int lastNumber = 64;
    public int lastOfLastNumber = 32;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }
    public void InitClassicCube()
    {
        int number = 2;
        if (GameManager.Instance.nextCube.cubeNumber > 0) number = GameManager.Instance.nextCube.cubeNumber;
        Cube newCube = CreateNewCube(defaultCubeSpawnPoint.position, true, number);
        newCube.initEffect.growEffect();
        GameManager.Instance.mainCube = newCube;
    }
    public Cube SpawnCubeX2(Vector3 spawnPoint, int number)
    {
        Cube newCube = CreateNewCube(spawnPoint, false, number);
        return newCube;
    }
    // ----------------- Helper Method -----------------
    public void SpawnClassicCube()
    {
        StartCoroutine(ISpawnClassicCube());
    }
    IEnumerator ISpawnClassicCube()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.mainCube == null)
        {
            InitClassicCube();
            GameManager.Instance.SpawnNextCube();
            GameManager.Instance.dataManager.Save();
        }
    }
    // ----------------- Create and Destroy -----------------
    private Cube CreateNewCube(Vector3 position, bool condition, int number)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", position, Quaternion.identity).GetComponent<Cube>();
        newCube.SetMainCube(condition);
        newCube.SetActiveLine(condition);
        newCube.trail.OffTrail();
        newCube.EditCube(number);

        listCube.AddCube(newCube);
        listCube.AddDataCube(newCube);
        return newCube;
    }

    public void SpawnCube(int number, Vector3 position, Quaternion rotation, bool isMainCube)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", position, rotation).GetComponent<Cube>();
        newCube.SetMainCube(isMainCube);
        if (isMainCube)
        {
            newCube.SetActiveLine(true);
            GameManager.Instance.mainCube = newCube;
        }
        else newCube.SetActiveLine(false);
        newCube.trail.OffTrail();
        newCube.EditCube(number);

        listCube.AddCube(newCube);
        listCube.AddDataCube(newCube);
    }

    public void DestroyCube(Cube cube)
    {
        cube.spawnEffect.StopEffect();
        ObjectPooler.Instance.ReturnToPool("ClassicCube", cube.gameObject);
        listCube.RemoveCube(cube);
        listCube.RemoveDataCube(cube);
    }
}
