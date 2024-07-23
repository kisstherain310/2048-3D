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
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "ClassicCube":
                HandleClassicCubeCollision(collision);
                break;
            case "JokerCube":
                HandleJokerCubeCollision(collision);
                break;
            case "BombCube":
                HandleBombCubeCollision(collision);
                break;
        }
    }
    // ---- Handle Collision --------------------------------
    private void HandleClassicCubeCollision(Collision collision)
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
            DestroyCube(cube); // Destroy main cube để kh có 2 cube trên vạch xuất phát cùng lúc
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
    private void HandleBombCubeCollision(Collision collision)
    {
        if(cube.isMainCube) {
            DestroyCube(cube); // Destroy main cube để kh có 2 cube trên vạch xuất phát cùng lúc
            return;
        };
        BombCube bombCube = collision.gameObject.GetComponent<BombCube>();
        if (bombCube != null)
        {
            DestroyBombCube(bombCube);
            DestroyCube(cube);

            HandleCollision(collision);                 
        }
    }
    private void HandleCollision(Collision collision, string typeCube){
            Vector3 contactPoint = collision.contacts[0].point;
            Cube newCubeX2 = GameManager.Instance.classicCubeManager.SpawnCubeX2(contactPoint + Vector3.up * 0.8f, cube.cubeNumber * 2);

            ProcessNewCube(newCubeX2, contactPoint);
            explosionForce(contactPoint);
            if(typeCube == "ClassicCube") {
                FXManager.Instance.PlayFX(contactPoint, newCubeX2.cubeUI.color, 0);

                FXManager.Instance.GetFX(1).transform.SetParent(newCubeX2.transform); // FX[1] gắn vào cubeX2
                FXManager.Instance.PlayFX(contactPoint, newCubeX2.cubeUI.color, 1);
            }
            else if(typeCube == "JokerCube") FXManager.Instance.PlayFX(contactPoint, 2);
    }
    private void HandleCollision(Collision collision){
            Vector3 contactPoint = collision.contacts[0].point;
            FXManager.Instance.PlayFX(contactPoint, 3);
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

    // ---- Destroy Cube --------------------------------
    private void DestroyCube(Cube cube)
    {
        GameManager.Instance.classicCubeManager.DestroyCube(cube);
    }
    private void DestroyJokerCube(JokerCube cube)
    {
        GameManager.Instance.jokerCubeManager.DestroyJokerCube(cube);
    }
    private void DestroyBombCube(BombCube cube)
    {
        GameManager.Instance.bombCubeManager.DestroyBombCube(cube);
    }
}
