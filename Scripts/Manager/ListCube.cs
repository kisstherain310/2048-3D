using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCube : MonoBehaviour
{
    [HideInInspector] public List<Cube> cubes;
    private void Awake()
    {
        cubes = new List<Cube>();
    }
    // ----------------- Helper Method -----------------
    public void AddCube(Cube cube)
    {
        cubes.Add(cube);
    }

    public void RemoveCube(Cube cube)
    {
        Cube cubeToRemove = cubes.Find(c => c.cubeUI == cube.cubeUI);
        if (cubeToRemove != null)
        {
            cubes.Remove(cubeToRemove);
        }
    }
    public Vector3 FindCubeNearest(Cube newCube)
    {
        float minDistance = Mathf.Infinity;
        Vector3 nearestCubePosition = Vector3.zero;
        foreach (Cube cube in cubes)
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
