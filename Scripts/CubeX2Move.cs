using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeX2Move : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float duration = 1f;

    public void moveToTarget(Vector3 targetPosition)
    {
        Debug.Log("Move to target");
        StartCoroutine(IMoveToTargetCoroutine(targetPosition));
    }

    private IEnumerator IMoveToTargetCoroutine(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position);
        direction = new Vector3(direction.x, 0, direction.z);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += speed * Time.deltaTime * direction;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
