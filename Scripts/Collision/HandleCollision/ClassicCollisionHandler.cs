using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicCollisionHandler : BaseCollisionHandler<Cube>
{
    public override void HandleCollision(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();
        if (otherCube != null && cube.CubeID > otherCube.CubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                VibrationManagerX.Vibrate();
                if(otherCube.transform.parent != null) DestroyCube(otherCube);
                if(cube.transform.parent != null) DestroyCube(cube);

                Vector3 contactPoint = collision.contacts[0].point;
                Cube newCubeX2 = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up * 0.4f, cube.cubeNumber * 2);
                SpawnPointCube(newCubeX2.transform.position, cube.cubeNumber * 2);
                newCubeX2.ToggleCollider(0.5f);

                ProcessNewCube(newCubeX2, contactPoint);
                ExplosionForce(contactPoint);

                FXManager.Instance.PlayFX(contactPoint, newCubeX2.cubeUI.color, FXType.ConfettiVFX);
                FXManager.Instance.GetFX(1).transform.SetParent(newCubeX2.transform);
                FXManager.Instance.PlayFX(contactPoint, newCubeX2.cubeUI.color, FXType.BlastVFX);
                SoundManager.instance.PlayComboHit(newCubeX2.cubeNumber);
            }
        }
    } 
}
