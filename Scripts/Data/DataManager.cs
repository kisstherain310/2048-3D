using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameState gameState;
    public List<InforListCube> inforListCubes;
    private List<Cube> cubes;
    public bool haveData = false;
    void Start()
    {
        LoadGameState();
    }
    void OnApplicationQuit()
    {
        SaveGameState();
    }
    private void SaveGameState(){
        inforListCubes = new List<InforListCube>();
        cubes = GameManager.Instance.listCube.cubes;
        foreach(Cube cube in cubes){
            InforListCube inforListCube = new InforListCube{
                number = cube.cubeNumber,
                position = cube.transform.position,
                rotation = cube.transform.rotation,
                isMainCube = cube.isMainCube,
            };
            inforListCubes.Add(inforListCube);
        }
        GameState gameState = new GameState{
            listCubes = inforListCubes
        };

        string json = JsonUtility.ToJson(gameState);
        string path = "data.json";
        // File.WriteAllText(Application.persistentDataPath + "/gameState.json", json);
        System.IO.File.WriteAllText(path, json);
    }
    private void LoadGameState()
    {
        // string path = Application.persistentDataPath + "/gameState.json";
        string path = "data.json";
        if (File.Exists(path))
        {
            haveData = true;
            string json = System.IO.File.ReadAllText(path);
            gameState = JsonUtility.FromJson<GameState>(json);
            if(gameState.listCubes == null) {
                haveData = false;
                return;
            }
            foreach(InforListCube inforListCube in gameState.listCubes){
                Vector3 position = Utilities.toVector3(inforListCube.position.x, inforListCube.position.y, inforListCube.position.z);
                Quaternion rotation = Utilities.toQuaternion(inforListCube.rotation.x, inforListCube.rotation.y, inforListCube.rotation.z, inforListCube.rotation.w);
                Debug.Log(inforListCube.isMainCube);
                GameManager.Instance.classicCubeManager.SpawnCube(inforListCube.number, position, rotation, inforListCube.isMainCube);
            }
        }  else haveData = false;                       
    }
    
}
