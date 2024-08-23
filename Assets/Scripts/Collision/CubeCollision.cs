using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private ClassicCollisionHandler classicHandler;
    private JokerCollisionHandler jokerHandler;
    private BombCollisionHandler bombHandler;

    void Awake()
    {
        classicHandler = gameObject.AddComponent<ClassicCollisionHandler>();
        jokerHandler = gameObject.AddComponent<JokerCollisionHandler>();
        bombHandler = gameObject.AddComponent<BombCollisionHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "ClassicCube":
                classicHandler.HandleCollision(collision);
                break;
            case "JokerCube":
                jokerHandler.HandleCollision(collision);
                break;
            case "BombCube":
                bombHandler.HandleCollision(collision);
                break;
        }
    }
}
