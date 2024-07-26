using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
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
        LoadGameState();
    }
    void OnApplicationQuit()
    {
        SaveGameState();
    }
    private void SaveGameState()
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
        nextCube = new InforClassicCube
        {
            number = GameManager.Instance.nextCube.cubeNumber,
            position = GameManager.Instance.nextCube.transform.position,
            rotation = GameManager.Instance.nextCube.transform.rotation,
            isMainCube = GameManager.Instance.nextCube.isMainCube,
        };
        score = GameManager.Instance.scoreManager.score;
        highScore = GameManager.Instance.scoreManager.highScore;
        GameState gameState = new GameState
        {
            listCubes = inforClassicCubes,
            listSpecialCubes = inforSpecialCubes,
            nextCube = nextCube,
            score = score,
            highScore = highScore,
        };

        string json = JsonUtility.ToJson(gameState);
        string path = "data.json";
        // File.WriteAllText(Application.persistentDataPath + "/gameState.json", json);
        System.IO.File.WriteAllText(path, json);
    }
    public bool CheckData()
    {
        string path = "data.json";
        return File.Exists(path);
    }
    private void LoadGameState()
    {
        // string path = Application.persistentDataPath + "/gameState.json";
        string path = "data.json";
        if (File.Exists(path))
        {
            Debug.Log("Load data");
            haveData = true;
            string json = System.IO.File.ReadAllText(path);
            gameState = JsonUtility.FromJson<GameState>(json);
            if(gameState.listCubes == null && gameState.listSpecialCubes == null) {
                haveData = false;
                return;
            }
            foreach (InforClassicCube inforCube in gameState.listCubes)
            {
                Vector3 position = Utilities.toVector3(inforCube.position.x, inforCube.position.y, inforCube.position.z);
                Quaternion rotation = Utilities.toQuaternion(inforCube.rotation.x, inforCube.rotation.y, inforCube.rotation.z, inforCube.rotation.w);
                GameManager.Instance.classicCubeManager.SpawnCube(inforCube.number, position, rotation, inforCube.isMainCube);
            }
            foreach (InforSpecialCube inforCube in gameState.listSpecialCubes)
            {
                string tag = inforCube.tag;
                Vector3 position = Utilities.toVector3(inforCube.position.x, inforCube.position.y, inforCube.position.z);
                Quaternion rotation = Utilities.toQuaternion(inforCube.rotation.x, inforCube.rotation.y, inforCube.rotation.z, inforCube.rotation.w);
                if (tag == "BombCube") GameManager.Instance.bombCubeManager.SpawnCube(tag, position, rotation, inforCube.isMainCube);
                if (tag == "JokerCube") GameManager.Instance.jokerCubeManager.SpawnCube(tag, position, rotation, inforCube.isMainCube);
            }
            GameManager.Instance.nextCube.EditCube(gameState.nextCube.number);
            GameManager.Instance.scoreManager.SetScore(gameState.score, gameState.highScore);
        }
        else haveData = false;
    }

}
