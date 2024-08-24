using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject level;
    [SerializeField] private GameObject otherLevel;
    private int levelIndex = 0;
    private int distance = 18;
    private Vector3 defaultPosition = new Vector3(0, 0, 5);
    private Vector3 spawnPosition = new Vector3(0, 0, 23);
    private void Awake()
    {
        level.GetComponent<LevelMove>().OnTaskCompleted.AddListener(OnTaskCompleted);
        otherLevel.GetComponent<LevelMove>().OnTaskCompleted.AddListener(OnTaskCompleted);
    }
    public GameObject GetMainLevel()
    {
        if(levelIndex == 0) return level;
        return otherLevel;
    }
    public void OnReplay()
    {
        if(levelIndex == 0)
        {
            GameManager.Instance.boardManager.SpawnBoard(otherLevel);
            otherLevel.GetComponent<LevelMove>().MoveEffect(spawnPosition, distance, true);
            level.GetComponent<LevelMove>().MoveEffect(defaultPosition, distance, false);
            levelIndex = 1;
        }
        else
        {
            GameManager.Instance.boardManager.SpawnBoard(level);
            level.GetComponent<LevelMove>().MoveEffect(spawnPosition, distance, true);
            otherLevel.GetComponent<LevelMove>().MoveEffect(defaultPosition, distance, false);
            levelIndex = 0;
        }
    }
    private void OnTaskCompleted(){
        DespawnLevel();
    }
    private void DespawnLevel()
    {
        GameManager.Instance.dataManager.SaveScoreUser();
        GameManager.Instance.listCube.RemoveAllCube();
        GameManager.Instance.boardManager.DestroyBoard(); 
        GameManager.Instance.scoreManager.EditScore();
        GameManager.Instance.SpawnNextCube();   
        GameManager.Instance.InitGame();
        GameManager.Instance.dataManager.SaveGameState();
    }
}
