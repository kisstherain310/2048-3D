using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeX2Move : MonoBehaviour
{
    [SerializeField] private float speed = 0.23f;
    private float duration = 1.35f;

    // Move to target position which is the position of the cube that the current cube nearest to
    public void moveToTarget(Vector3 targetPosition)
    {
        StartCoroutine(IMoveToTargetCoroutine(targetPosition));
    }

    private IEnumerator IMoveToTargetCoroutine(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position);
        float up = Random.Range(0.8f, 1.2f);
        direction = new Vector3(direction.x, up, direction.z);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += speed * Time.deltaTime * direction;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
