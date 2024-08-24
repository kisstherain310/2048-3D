using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeX2Move : MonoBehaviour
{
    [SerializeField] private float speed = 0.23f;
    private float duration = 1.35f;
    private Rigidbody rb;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Move to target position, stopping at a random distance
    public void moveToTarget(Vector3 targetPosition)
    {
        animator.Play("Jump");

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
            // Move the Rigidbody towards the target position
            rb.MovePosition(rb.position + speed * Time.deltaTime * direction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.Play("down");
    }
}
