using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerCollisionHandler : BaseCollisionHandler<JokerCube>
{
    public override void DestroyCube(JokerCube cube)
    {
        GameManager.Instance.jokerCubeManager.DestroyJokerCube(cube);
    }
    public override void HandleCollision(Collision collision)
    {
        if(cube.isMainCube) {
            DestroyCube(cube); // Destroy main cube để kh có 2 cube trên vạch xuất phát cùng lúc
            return;
        };
        JokerCube jokerCube = collision.gameObject.GetComponent<JokerCube>();
        if (jokerCube != null)
        {
            DestroyCube(jokerCube);
            DestroyCube(cube);

            Vector3 contactPoint = collision.contacts[0].point;
            Cube newCubeX2 = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up * 0.8f, cube.cubeNumber * 2);
            SpawnPointCube(newCubeX2.transform.position, cube.cubeNumber * 2, newCubeX2.cubeUI.color);

            ProcessNewCube(newCubeX2, contactPoint);
            ExplosionForce(contactPoint);
            FXManager.Instance.PlayFX(contactPoint, 2);     
        }
    }
}
