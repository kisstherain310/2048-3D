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
        if (cube.isMainCube)
        {
            DestroyCube(cube); // Destroy main cube để kh có 2 cube trên vạch xuất phát cùng lúc
            return;
        };
        JokerCube jokerCube = collision.gameObject.GetComponent<JokerCube>();
        if (jokerCube != null)
        {
            VibrationManagerX.Vibrate();
            Vector3 contactPoint = collision.contacts[0].point;
            if(jokerCube.transform.parent != null) DestroyCube(jokerCube);
            if(cube.transform.parent != null) DestroyCube(cube);
            Cube newCubeX2 = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up * 0.4f, cube.cubeNumber * 2);

            SpawnPointCube(newCubeX2.transform.position, newCubeX2.cubeNumber);

            ProcessNewCube(newCubeX2, contactPoint);
            ExplosionForce(contactPoint);
            FXManager.Instance.PlayFX(contactPoint, FXType.JokerEffect);
            FXManager.Instance.GetFX(1).transform.SetParent(newCubeX2.transform);
            FXManager.Instance.PlayFX(contactPoint, newCubeX2.cubeUI.color, FXType.BlastVFX);
            SoundManager.instance.PlayClip(AudioType.JokerExplore);
        }
    }
}
