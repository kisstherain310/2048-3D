using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBombCube : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameManager.Instance.bombCubeManager.SpawnBombCube();
    }
}
