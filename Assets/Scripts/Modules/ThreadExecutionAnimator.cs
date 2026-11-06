
using System.Collections;
using UnityEngine;

public class ThreadExecutionAnimator : MonoBehaviour
{
    [Header("3D Thread Objects")]
    public Transform threadA;
    public Transform threadB;

    [Header("Animation Settings")]
    public float moveHeight = 0.04f;
    public float animationDuration = 0.4f;

    private Vector3 originalAPosition;
    private Vector3 originalBPosition;

    private Coroutine animationA;
    private Coroutine animationB;

    private void Awake()
    {
        if (threadA != null)
            originalAPosition = threadA.localPosition;

        if (threadB != null)
            originalBPosition = threadB.localPosition;
    }

    public void AnimateThreadA()
    {
        if (threadA == null) return;

        if (animationA != null)
            StopCoroutine(animationA);

        threadA.localPosition = originalAPosition;

        animationA = StartCoroutine(
            AnimateThread(threadA, originalAPosition)
        );
    }

    public void AnimateThreadB()
    {
        if (threadB == null) return;

        if (animationB != null)
            StopCoroutine(animationB);

        threadB.localPosition = originalBPosition;

        animationB = StartCoroutine(
            AnimateThread(threadB, originalBPosition)
        );
    }

    private IEnumerator AnimateThread(
        Transform thread,
        Vector3 originalPosition)
    {
        Vector3 raisedPosition =
            originalPosition + Vector3.up * moveHeight;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float progress =
                Mathf.Clamp01(elapsed / animationDuration);

            float bounce =
                Mathf.Sin(progress * Mathf.PI);

            thread.localPosition = Vector3.Lerp(
                originalPosition,
                raisedPosition,
                bounce
            );

            yield return null;
        }

        thread.localPosition = originalPosition;
    }

    public void ResetAnimations()
    {
        if (animationA != null)
            StopCoroutine(animationA);

        if (animationB != null)
            StopCoroutine(animationB);

        if (threadA != null)
            threadA.localPosition = originalAPosition;

        if (threadB != null)
            threadB.localPosition = originalBPosition;
    }
}
