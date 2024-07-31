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
    void Start()
    {
        // LoadGameState();
    }

    public void Save()
    {
        // SaveGameState();
    }
    private void SaveGameState()
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

    private void LoadGameState()
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
        foreach (BaseCube cube in dataCubes)
        {
            if (cube.poolTag == "ClassicCube")
            {
                InforClassicCube inforCube = new InforClassicCube
                {
                    number = cube.GetComponent<Cube>().cubeNumber,
                    position = cube.transform.position,
                    rotation = cube.transform.rotation,
                    isMainCube = cube.isMainCube,
                };
                inforClassicCubes.Add(inforCube);
            }
            else
            {
                InforSpecialCube inforCube = new InforSpecialCube
                {
                    tag = cube.poolTag,
                    position = cube.transform.position,
                    rotation = cube.transform.rotation,
                    isMainCube = cube.isMainCube,
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
        foreach (InforClassicCube inforCube in gameState.listCubes)
        {
            Vector3 position = Utilities.toVector3(inforCube.position.x, inforCube.position.y, inforCube.position.z);
            Quaternion rotation = Utilities.toQuaternion(inforCube.rotation.x, inforCube.rotation.y, inforCube.rotation.z, inforCube.rotation.w);
            GameManager.Instance.classicCubeManager.SpawnCube(inforCube.number, position, rotation, inforCube.isMainCube);
        }
    }
    private void LoadSpecialCube(GameState gameState)
    {
        foreach (InforSpecialCube inforCube in gameState.listSpecialCubes)
        {
            string tag = inforCube.tag;
            Vector3 position = Utilities.toVector3(inforCube.position.x, inforCube.position.y, inforCube.position.z);
            Quaternion rotation = Utilities.toQuaternion(inforCube.rotation.x, inforCube.rotation.y, inforCube.rotation.z, inforCube.rotation.w);
            if (tag == "BombCube") GameManager.Instance.bombCubeManager.SpawnCube(tag, position, rotation, inforCube.isMainCube);
            if (tag == "JokerCube") GameManager.Instance.jokerCubeManager.SpawnCube(tag, position, rotation, inforCube.isMainCube);
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
