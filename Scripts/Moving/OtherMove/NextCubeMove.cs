using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCubeMove : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    private void Awake()
    {
        startPosition = transform.position + new Vector3(2, 0, 0);
        endPosition = transform.position;
        transform.position = startPosition; 
    }
    public void MoveEffect()
    {
        StartCoroutine(MoveEffectCoroutine());
    }
    private IEnumerator MoveEffectCoroutine()
    {
        float elapsedTime = 0.0f;
        float totalTime = 0.5f;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
    }
}
