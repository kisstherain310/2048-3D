using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    public void MoveEffect(Vector3 pointStart, Vector3 pointEnd)
    {
        StartCoroutine(MoveEffectCoroutine(pointStart, pointEnd));
    }
    IEnumerator MoveEffectCoroutine(Vector3 pointStart, Vector3 pointEnd) // A -> B -> C -> B -> D -> B
    {
        Vector3 A = pointStart;
        Vector3 B = pointEnd;
        Vector3 C = A + (B - A) * 3 / 4;
        Vector3 D = C + (B - C) * 3 / 4;

        float totalTime = 1.0f;
        float halfTime = totalTime / 1.625f;
        float quarterTime = totalTime * 0.25f / 1.625f;
        float elapsedTime = 0.0f;
        transform.position = A;

        while (elapsedTime < quarterTime)
        {
            transform.position = Vector3.Lerp(A, B, elapsedTime / halfTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = B;
        elapsedTime = 0.0f;

        while (elapsedTime < quarterTime)
        {
            transform.position = Vector3.Lerp(B, C, elapsedTime / quarterTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = C;
        elapsedTime = 0.0f;

        while (elapsedTime < quarterTime)
        {
            transform.position = Vector3.Lerp(C, B, elapsedTime / quarterTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = B;
        elapsedTime = 0.0f;

        while (elapsedTime < quarterTime)
        {
            transform.position = Vector3.Lerp(B, D, elapsedTime / quarterTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = D;
        elapsedTime = 0.0f;

        while (elapsedTime < quarterTime)
        {
            transform.position = Vector3.Lerp(D, B, elapsedTime / quarterTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = B;
    }
}
