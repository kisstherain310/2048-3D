using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    [SerializeField] private float momen;
    [SerializeField] private float jumpForce = 10f;
    private Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }
    // ---- Collision --------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ClassicCube"))
        {
            HandleCubeCollision(collision);
        }
        else if (collision.gameObject.CompareTag("JokerCube"))
        {
            HandleJokerCubeCollision(collision);
        }
    }
    // ---- Handle Collision --------------------------------
    private void HandleCubeCollision(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();
        if (otherCube != null && cube.CubeID > otherCube.CubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                DestroyCube(otherCube);
                DestroyCube(cube);

                HandleCollision(collision, "ClassicCube");
            }
        }
    }

    private void HandleJokerCubeCollision(Collision collision)
    {
        if(cube.isMainCube) {
            DestroyCube(cube);
            return;
        };
        JokerCube jokerCube = collision.gameObject.GetComponent<JokerCube>();
        if (jokerCube != null)
        {
            DestroyJokerCube(jokerCube);
            DestroyCube(cube);

            HandleCollision(collision, "JokerCube");            
        }
    }
    private void HandleCollision(Collision collision, string typeCube){
            Vector3 contactPoint = collision.contacts[0].point;
            Cube newCubeX2 = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up * 0.8f, cube.cubeNumber * 2);

            ProcessNewCube(newCubeX2, contactPoint);
            explosionForce(contactPoint);
            if(typeCube == "ClassicCube") {
                PlayFX(newCubeX2, contactPoint, 0, false);
                PlayFX(newCubeX2, contactPoint, 1, true);
            }
            else if(typeCube == "JokerCube") PlayFX(newCubeX2, contactPoint, 2, false);
    }
    // ---- Helper Methods --------------------------------
    private void ProcessNewCube(Cube newCube, Vector3 contactPoint)
    {
        // Jump effect
        Vector3 direction = new Vector3(0, 2f, 0.5f);
        newCube.rb.AddForce(direction * jumpForce, ForceMode.Impulse); 
        newCube.spawnEffect.ExploreEffect();

        // Move to target
        Vector3 targetMove = GameManager.Instance.FindCubeNearest(newCube);
        if(targetMove != Vector3.zero) newCube.cubeX2Move.moveToTarget(targetMove);

        // Random rotation
        float randomValue = Random.Range(-momen, momen);
        if (randomValue < 40) randomValue = 40;
        Vector3 randomDirection = Vector3.one * randomValue;
        newCube.rb.AddTorque(randomDirection);
    }
    private void PlayFX(Cube newCube, Vector3 contactPoint, int number, bool isSetParent)
    {
        if(isSetParent) FXManager.Instance.GetFX(number).transform.SetParent(newCube.transform);
        FXManager.Instance.PlayCubeExplosionFX(contactPoint, newCube.cubeUI.color, number);
    }
    private void explosionForce(Vector3 contactPoint)
    {
        Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 1f);
        float explosionForce = 50f;
        float explosionRadius = 1.1f;
        foreach (Collider coll in surroundedCubes)
        {
            if (coll.attachedRigidbody != null)
                coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
        }
    }
    private void DestroyCube(Cube cube)
    {
        GameManager.Instance.classicCubeManager.DestroyCube(cube);
    }
    private void DestroyJokerCube(JokerCube cube)
    {
        GameManager.Instance.jokerCubeManager.DestroyJokerCube(cube);
    }
}
