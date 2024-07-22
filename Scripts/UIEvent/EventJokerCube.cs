using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventJokerCube : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.jokerCubeManager.SpawnJokerCube();
    }
}
