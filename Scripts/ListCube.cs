using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCube : MonoBehaviour
{
    public List<Cube> cubes;
    private void Awake()
    {
        cubes = new List<Cube>();
    }
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
}
