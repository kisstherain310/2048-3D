using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Vector3 position = new Vector3(0, -0.65f, 2.88f);
    private GameObject oldBoard = null;
    private GameObject newBoard = null;
    public void SpawnBoard(GameObject boardParent){
        oldBoard = newBoard;
        newBoard = ObjectPooler.Instance.SpawnFromPool("Board", position, Quaternion.identity);
        newBoard.transform.SetParent(boardParent.transform);
    }
    public void DestroyBoard(){
        oldBoard.transform.SetParent(null);
        ObjectPooler.Instance.ReturnToPool("Board", oldBoard);
    }
}
