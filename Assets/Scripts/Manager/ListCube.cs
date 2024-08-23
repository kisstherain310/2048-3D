using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCube : MonoBehaviour
{
    [HideInInspector] public List<Cube> cubes;
    [HideInInspector] public List<BaseCube> dataCubes;
    void Awake()
    {
        cubes = new List<Cube>();
    }
    // ----------------- Helper Method -----------------
    // ----------------- Add  -----------------
    public void AddCube(Cube cube) => cubes.Add(cube);
    public void AddDataCube(BaseCube cube) => dataCubes.Add(cube);
    // ----------------- Remove  -----------------
    public void RemoveCube(Cube cube)
    {
        Cube newCube = cubes.Find(x => x.CubeID == cube.CubeID);
        cubes.Remove(newCube);
    }
    public void RemoveDataCube(BaseCube cube)
    {
        BaseCube newCube = dataCubes.Find(x => x.CubeID == cube.CubeID);
        dataCubes.Remove(newCube);
    }
    public void RemoveAllCube()
    {
        while(dataCubes.Count > 0)
        {
            if(dataCubes[0].poolTag == "ClassicCube") GameManager.Instance.classicCubeManager.DestroyCube(dataCubes[0].GetComponent<Cube>());
            else if(dataCubes[0].poolTag == "BombCube") GameManager.Instance.bombCubeManager.DestroyCube(dataCubes[0].GetComponent<BombCube>());
            else if(dataCubes[0].poolTag == "JokerCube") GameManager.Instance.jokerCubeManager.DestroyCube(dataCubes[0].GetComponent<JokerCube>());
        }
        cubes.Clear();
        dataCubes.Clear();
    }
    public Vector3 FindCubeNearest(Cube newCube)
    {
        float minDistance = Mathf.Infinity;
        Vector3 nearestCubePosition = Vector3.zero;
        for(int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].isMainCube) continue;
            if (cubes[i].cubeNumber == newCube.cubeNumber)
            {
                float distance = Vector3.Distance(cubes[i].transform.position, newCube.transform.position);
                if (distance < minDistance && distance > 0)
                {
                    minDistance = distance;
                    nearestCubePosition = cubes[i].transform.position;
                }
            }
        }
        return nearestCubePosition;
    }
}
