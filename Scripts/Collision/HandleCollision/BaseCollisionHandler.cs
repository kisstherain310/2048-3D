using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class BaseCollisionHandler<T> : MonoBehaviour
{
    protected Cube cube;
    protected float jumpForce = 3f;
    protected float momen = 50f;

    protected virtual void Awake()
    {
        cube = GetComponent<Cube>();
    }

    public abstract void HandleCollision(Collision collision);
    protected void ProcessNewCube(Cube newCube, Vector3 contactPoint)
    {
        // Jump effect
        Vector3 direction = new Vector3(0, 2f, 0.5f);
        newCube.rb.AddForce(direction * jumpForce, ForceMode.Impulse); 
        newCube.spawnEffect.ExploreEffect();

        // Move to target
        Vector3 targetMove = GameManager.Instance.listCube.FindCubeNearest(newCube);
        if(targetMove != Vector3.zero) newCube.cubeX2Move.moveToTarget(targetMove);

        // Random rotation
        float randomValue = Random.Range(-momen, momen);
        if (randomValue < 40) randomValue = 40;
        Vector3 randomDirection = Vector3.one * randomValue;
        newCube.rb.AddTorque(randomDirection);
    }
    protected void SpawnPointCube(Vector3 position, int point, Color color)
    {
        GameManager.Instance.pointCubeManager.SpawnPointCube(position, point, color);
    }
    protected void ExplosionForce(Vector3 contactPoint)
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
    public virtual void DestroyCube(T cube){}
    protected void DestroyCube(Cube cube)
    {
        GameManager.Instance.classicCubeManager.DestroyCube(cube);
    }
}
