using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollisionHandler : BaseCollisionHandler<BombCube>
{
    public override void DestroyCube(BombCube cube)
    {
        GameManager.Instance.bombCubeManager.DestroyBombCube(cube);
    }
    public override void HandleCollision(Collision collision)
    {
        if(cube.isMainCube) {
            DestroyCube(cube); // Destroy main cube để kh có 2 cube trên vạch xuất phát cùng lúc
            return;
        };
        BombCube bombCube = collision.gameObject.GetComponent<BombCube>();
        if (bombCube != null)
        {
            SpawnPointCube(cube.transform.position, cube.cubeNumber, cube.cubeUI.color);
            DestroyCube(bombCube);
            DestroyCube(cube);

            Vector3 contactPoint = collision.contacts[0].point;
            FXManager.Instance.PlayFX(contactPoint, 3);               
        }
    }
}
