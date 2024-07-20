using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicCubeManager : MonoBehaviour
{
    private ListCube listCube;
    private Transform defaultCubeSpawnPoint;

    public void getListCube(ListCube listCube)
    {
        this.listCube = listCube;
    }
    public void getDefaultCubeSpawnPoint(Transform defaultCubeSpawnPoint)
    {
        this.defaultCubeSpawnPoint = defaultCubeSpawnPoint;
    }
    public void InitClassicCube(){
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", defaultCubeSpawnPoint.position, Quaternion.identity).GetComponent<Cube>();
        int number = GenerateRandomNumber();    
        newCube.SetMainCube(true);
        newCube.SetActiveLine(true);
        newCube.EditCube(number);

        listCube.AddCube(newCube);
    }

    public void SpawnClassicCube()
    {
        StartCoroutine(ISpawnClassicCube());
    }
    IEnumerator ISpawnClassicCube(){
        yield return new WaitForSeconds(0.3f);
        InitClassicCube();
    }
    public Cube SpawnCubeX2(Vector3 spawnPoint, int number)
    {
        Cube newCube = ObjectPooler.Instance.SpawnFromPool("ClassicCube", spawnPoint, Quaternion.identity).GetComponent<Cube>();  
        newCube.SetMainCube(false);
        newCube.SetActiveLine(false);
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
        return (int)Mathf.Pow (2, Random.Range (1, 4)) ;
    }
}
