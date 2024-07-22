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
}
