using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollisionHandler<T> : MonoBehaviour
{
    protected Cube cube;
    protected float jumpForce = 3f;
    protected float momen = 45f;
    protected float minMomen = 30f;

    void Awake()
    {
        cube = GetComponent<Cube>();
    }

    public abstract void HandleCollision(Collision collision);
    protected void ProcessNewCube(Cube newCube, Vector3 contactPoint)
    {
        // Jump effect
        Vector3 direction = new Vector3(0, 2f, 1f);
        newCube.rb.AddForce(direction * jumpForce, ForceMode.Impulse);
        newCube.spawnEffect.ExploreEffect();

        // Move to target
        Vector3 targetMove = GameManager.Instance.listCube.FindCubeNearest(newCube);
        if (targetMove != Vector3.zero) newCube.cubeX2Move.moveToTarget(targetMove);

        // Random rotation
        Vector3 randomDirection = GetRandomVector3() * 50f;
        newCube.rb.AddTorque(randomDirection);
    }
    private Vector3 GetRandomVector3()
    {
        float x = Random.Range(-3f, -1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        return new Vector3(x, y, z);
    }
    protected void SpawnPointCube(Vector3 position, int point)
    {
        GameManager.Instance.pointCubeManager.SpawnPointCube(position, point);
    }
    protected void ExplosionForce(Vector3 contactPoint)
    {
        Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 1f);
        float explosionForce = 30f;
        float explosionRadius = 1.05f;
        for (int i = 0; i < surroundedCubes.Length; i++)
        {
            if (surroundedCubes[i].attachedRigidbody != null)
                surroundedCubes[i].attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
        }
    }
    public virtual void DestroyCube(T cube) { }
    protected void DestroyCube(Cube cube)
    {
        if (cube.transform.parent != null) GameManager.Instance.classicCubeManager.DestroyCube(cube);
    }
}
