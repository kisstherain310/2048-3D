using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelMove : MonoBehaviour
{
    private float totalTime = 1f;
    public UnityEvent OnTaskCompleted;
    public void MoveEffect(Vector3 pointStart, int distance, bool isMain)
    {
        Vector3 pointEnd = new Vector3(pointStart.x, pointStart.y, pointStart.z - distance);
        StartCoroutine(MoveEffectCoroutine(pointStart, pointEnd, isMain));
    }
    private IEnumerator MoveEffectCoroutine(Vector3 pointStart, Vector3 pointEnd, bool isMain) 
    {
        Vector3 A = pointStart;
        Vector3 B = pointEnd;

        float elapsedTime = 0.0f;
        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(A, B, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = B;
        if(isMain) OnTaskCompleted?.Invoke();
        else transform.position = pointStart; 
    }
}
