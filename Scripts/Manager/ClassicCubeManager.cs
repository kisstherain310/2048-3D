using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicCubeManager : MonoBehaviour
{
    private ListCube listCube;
    private Transform defaultCubeSpawnPoint;

    public void Initialize(ListCube listCube, Transform defaultCubeSpawnPoint)
    {
        this.listCube = listCube;
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }
    public void InitClassicCube(){
        Cube newCube = CreateNewCube(defaultCubeSpawnPoint.position, true, GenerateRandomNumber());
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
    IEnumerator ISpawnClassicCube(){
        yield return new WaitForSeconds(0.5f);
        InitClassicCube();
    }
    // ----------------- Create and Destroy -----------------
    private Cube CreateNewCube(Vector3 position, bool condition, int number)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", position, Quaternion.identity).GetComponent<Cube>();
        newCube.SetMainCube(condition);
        newCube.SetActiveLine(condition);
        newCube.EditCube(number);

        listCube.AddCube(newCube);
        return newCube;
    }

    public void DestroyCube(Cube cube)
    {
        ObjectPooler.Instance.ReturnToPool("ClassicCube", cube.gameObject);
        listCube.RemoveCube(cube);
    }

    public int GenerateRandomNumber () {
        return (int)Mathf.Pow (2, Random.Range (1, 5)) ;
    }
}
