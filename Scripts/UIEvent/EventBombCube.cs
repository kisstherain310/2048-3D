using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBombCube : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.bombCubeManager.SpawnBombCube();
    }
}
