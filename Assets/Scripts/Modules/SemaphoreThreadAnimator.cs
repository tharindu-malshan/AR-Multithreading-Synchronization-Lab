
using System.Collections;
using UnityEngine;

public class SemaphoreThreadAnimator : MonoBehaviour
{
    [Header("Thread Models")]
    [SerializeField] private Transform threadA;
    [SerializeField] private Transform threadB;
    [SerializeField] private Transform threadC;

    [Header("Waiting Indicator")]
    [SerializeField] private GameObject threadCWaitingIndicator;

    [Header("Animation Settings")]
    [SerializeField] private float animationHeight = 0.04f;
    [SerializeField] private float animationDuration = 0.4f;

    private Vector3 threadAStart;
    private Vector3 threadBStart;
    private Vector3 threadCStart;

    private Coroutine animationA;
    private Coroutine animationB;
    private Coroutine animationC;

    private void Awake()
    {
        if (threadA != null)
            threadAStart = threadA.localPosition;

        if (threadB != null)
            threadBStart = threadB.localPosition;

        if (threadC != null)
            threadCStart = threadC.localPosition;
    }

    public void AnimateThreadA()
    {
        if (threadA == null) return;

        if (animationA != null)
            StopCoroutine(animationA);

        threadA.localPosition = threadAStart;
        animationA = StartCoroutine(
            AnimateThread(threadA, threadAStart));
    }

    public void AnimateThreadB()
    {
        if (threadB == null) return;

        if (animationB != null)
            StopCoroutine(animationB);

        threadB.localPosition = threadBStart;
        animationB = StartCoroutine(
            AnimateThread(threadB, threadBStart));
    }

    public void AnimateThreadC()
    {
        if (threadC == null) return;

        if (animationC != null)
            StopCoroutine(animationC);

        threadC.localPosition = threadCStart;
        animationC = StartCoroutine(
            AnimateThread(threadC, threadCStart));
    }

    public void ShowThreadCWaiting()
    {
        if (threadCWaitingIndicator != null)
            threadCWaitingIndicator.SetActive(true);
    }

    public void HideThreadCWaiting()
    {
        if (threadCWaitingIndicator != null)
            threadCWaitingIndicator.SetActive(false);
    }

    public void ResetAnimations()
    {
        if (animationA != null)
            StopCoroutine(animationA);

        if (animationB != null)
            StopCoroutine(animationB);

        if (animationC != null)
            StopCoroutine(animationC);

        if (threadA != null)
            threadA.localPosition = threadAStart;

        if (threadB != null)
            threadB.localPosition = threadBStart;

        if (threadC != null)
            threadC.localPosition = threadCStart;

        HideThreadCWaiting();
    }

    private IEnumerator AnimateThread(
        Transform target,
        Vector3 startPosition)
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / Mathf.Max(animationDuration, 0.01f));

            float offset = Mathf.Sin(t * Mathf.PI)
                           * animationHeight;

            target.localPosition =
                startPosition + Vector3.up * offset;

            yield return null;
        }

        target.localPosition = startPosition;
    }
}
