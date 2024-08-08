using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevelManager : MonoBehaviour
{
    public InitLevel[] listLevel;
    private int[][]  listNumberCube;

    void Awake(){
        listNumberCube = new int[listLevel.Length][];
        listNumberCube[0] = new int[]{2, 2, 2, 2, 4, 4};
        listNumberCube[1] = new int[]{2, 2, 2, 2, 4, 2, 4, 2};
        listNumberCube[2] = new int[]{2, 2, 4, 4, 2, 2};
    }
    public void SpawnLevel(int level)
    {
        for (int i = 0; i < listLevel[level].listCube.Length; i++)
        {
            GameManager.Instance.classicCubeManager.SpawnCube(listNumberCube[level][i], listLevel[level].listCube[i].transform.position, Quaternion.identity, false);
        }
        Invoke("InitializeCube", 2f);
    }
    private void InitializeCube(){
        if(GameManager.Instance.mainCube == null) GameManager.Instance.classicCubeManager.InitializeCube();
        GameManager.Instance.gameStatus.OnPlay();
    }
}
