using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string GameState
    {
        get { return PlayerPrefs.GetString("GameState", ""); }
        set { PlayerPrefs.SetString("GameState", value); }
    }
    public GameState gameState;
    private List<InforClassicCube> inforClassicCubes;
    private List<InforSpecialCube> inforSpecialCubes;
    private InforClassicCube nextCube;
    private int score;
    private int highScore;
    private List<BaseCube> dataCubes;
    private bool haveData = false;

    public void SaveGameState()
    {
        SaveCube();
        SaveNextCube();
        SaveScore();
        GameState gameState = new GameState
        {
            listCubes = inforClassicCubes,
            listSpecialCubes = inforSpecialCubes,
            nextCube = nextCube,
            score = score,
            highScore = highScore,
        };

        GameState = JsonUtility.ToJson(gameState);
    }

    public void LoadGameState()
    {
        if (GameState.Length > 0)
        {
            haveData = true;
            gameState = JsonUtility.FromJson<GameState>(GameState);
            if (gameState.listCubes == null && gameState.listSpecialCubes == null)
            {
                haveData = false;
                return;
            }
            LoadCube(gameState);
            LoadSpecialCube(gameState);
            LoadNextCube(gameState);
            LoadScore(gameState);
        }
        else haveData = false;

    }
    private void SaveCube()
    {
        inforClassicCubes = new List<InforClassicCube>();
        inforSpecialCubes = new List<InforSpecialCube>();
        dataCubes = GameManager.Instance.listCube.dataCubes;
        for(int i = 0; i < dataCubes.Count; i++)
        {
            if (dataCubes[i].poolTag == "ClassicCube")
            {
                InforClassicCube inforCube = new InforClassicCube
                {
                    number = dataCubes[i].GetComponent<Cube>().cubeNumber,
                    position = dataCubes[i].transform.position,
                    rotation = dataCubes[i].transform.rotation,
                    isMainCube = dataCubes[i].isMainCube,
                };
                inforClassicCubes.Add(inforCube);
            }
            else
            {
                InforSpecialCube inforCube = new InforSpecialCube
                {
                    tag = dataCubes[i].poolTag,
                    position = dataCubes[i].transform.position,
                    rotation = dataCubes[i].transform.rotation,
                    isMainCube = dataCubes[i].isMainCube,
                };
                inforSpecialCubes.Add(inforCube);
            }
        }
    }
    private void SaveNextCube()
    {
        nextCube = new InforClassicCube
        {
            number = GameManager.Instance.nextCube.cubeNumber,
            position = GameManager.Instance.nextCube.transform.position,
            rotation = GameManager.Instance.nextCube.transform.rotation,
            isMainCube = GameManager.Instance.nextCube.isMainCube,
        };
    }
    private void SaveScore()
    {
        score = GameManager.Instance.scoreManager.score;
        highScore = GameManager.Instance.scoreManager.highScore;
    }
    private void LoadCube(GameState gameState)
    {
        for(int i = 0; i < gameState.listCubes.Count; i++)
        {
            Vector3 position = Utilities.toVector3(gameState.listCubes[i].position.x, gameState.listCubes[i].position.y, gameState.listCubes[i].position.z);
            Quaternion rotation = Utilities.toQuaternion(gameState.listCubes[i].rotation.x, gameState.listCubes[i].rotation.y, gameState.listCubes[i].rotation.z, gameState.listCubes[i].rotation.w);
            GameManager.Instance.classicCubeManager.SpawnCube(gameState.listCubes[i].number, position, rotation, gameState.listCubes[i].isMainCube);
        }
    }
    private void LoadSpecialCube(GameState gameState)
    {
        for(int i = 0; i < gameState.listSpecialCubes.Count; i++)
        {
            string tag = gameState.listSpecialCubes[i].tag;
            Vector3 position = Utilities.toVector3(gameState.listSpecialCubes[i].position.x, gameState.listSpecialCubes[i].position.y, gameState.listSpecialCubes[i].position.z);
            Quaternion rotation = Utilities.toQuaternion(gameState.listSpecialCubes[i].rotation.x, gameState.listSpecialCubes[i].rotation.y, gameState.listSpecialCubes[i].rotation.z, gameState.listSpecialCubes[i].rotation.w);
            if (tag == "BombCube") GameManager.Instance.bombCubeManager.SpawnCube(tag, position, rotation, gameState.listSpecialCubes[i].isMainCube);
            if (tag == "JokerCube") GameManager.Instance.jokerCubeManager.SpawnCube(tag, position, rotation, gameState.listSpecialCubes[i].isMainCube);
        }
    }
    private void LoadNextCube(GameState gameState)
    {
        GameManager.Instance.nextCube.EditCube(gameState.nextCube.number);
    }
    private void LoadScore(GameState gameState)
    {
        GameManager.Instance.scoreManager.SetScore(gameState.score, gameState.highScore);
    }
    public bool CheckData()
    {
        return GameState.Length > 0;
    }
}
