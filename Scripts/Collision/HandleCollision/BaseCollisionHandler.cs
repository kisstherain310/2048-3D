using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class BaseCollisionHandler<T> : MonoBehaviour
{
    protected Cube cube;
    protected float jumpForce = 3f;
    protected float momen = 90f;
    protected float minMomen = 60f;

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
        float randomValue = Random.Range(0, momen);
        if (randomValue < minMomen) randomValue = minMomen;
        Vector3 randomDirection = GetRandomVector3() * randomValue;
        newCube.rb.AddTorque(randomDirection);
    }
    private Vector3 GetRandomVector3()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        return new Vector3(x, y, z);
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
