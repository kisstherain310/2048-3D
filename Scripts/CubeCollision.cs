using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    [SerializeField] private float momen;
    private Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && cube.CubeID > otherCube.CubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                GameManager.Instance.classicCubeManager.DestroyCube(otherCube);
                GameManager.Instance.classicCubeManager.DestroyCube(cube);

                Vector3 contactPoint = collision.contacts[0].point;
                Cube newCube = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up, cube.cubeNumber * 2);
                FXManager.Instance.GetFX().transform.SetParent(newCube.transform);
                Vector3 targetMove = GameManager.Instance.FindCubeNearest(newCube);
                if(targetMove != Vector3.zero) newCube.cubeX2Move.moveToTarget(targetMove);

                float randomValue = Random.Range(-momen, momen);
                if (randomValue < 20) randomValue = 12;
                Vector3 randomDirection = Vector3.one * randomValue;
                newCube.rb.AddTorque(randomDirection);

                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 1f);
                float explosionForce = 500f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                }

                FXManager.Instance.PlayCubeExplosionFX(contactPoint, newCube.cubeUI.color);
            }
        }
    }
}
