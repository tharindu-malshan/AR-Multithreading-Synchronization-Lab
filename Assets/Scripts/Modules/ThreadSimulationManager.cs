
using System.Collections;
using UnityEngine;
using TMPro;

public class ThreadSimulationManager : MonoBehaviour
{
    public enum ThreadState
    {
        New,
        Ready,
        Running,
        Waiting,
        Terminated
    }

    [Header("Thread A")]
    [SerializeField] private GameObject threadA;
    [SerializeField] private Renderer threadAStatusLight;

    [Header("Lifecycle State Markers")]
    [SerializeField] private Renderer stateNew;
    [SerializeField] private Renderer stateReady;
    [SerializeField] private Renderer stateRunning;
    [SerializeField] private Renderer stateTerminated;

    [Header("Optional Status UI")]
    [SerializeField] private TMP_Text statusText;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float raisedHeight = 0.025f;
    [SerializeField] private float brightness = 1.5f;

    private ThreadState currentState = ThreadState.New;

    private Renderer[] markers;
    private Material[] markerMaterials;
    private Color[] originalColors;
    private Vector3[] originalPositions;

    private Material statusMaterial;
    private Coroutine animationRoutine;

    private readonly Color newColor =
        new Color32(148, 163, 184, 255);

    private readonly Color readyColor =
        new Color32(245, 183, 66, 255);

    private readonly Color runningColor =
        new Color32(0, 255, 153, 255);

    private readonly Color waitingColor =
        new Color32(255, 152, 0, 255);

    private readonly Color terminatedColor =
        new Color32(239, 68, 68, 255);

    private void Awake()
    {
        markers = new Renderer[]
        {
            stateNew,
            stateReady,
            stateRunning,
            stateTerminated
        };

        markerMaterials = new Material[markers.Length];
        originalColors = new Color[markers.Length];
        originalPositions = new Vector3[markers.Length];

        for (int i = 0; i < markers.Length; i++)
        {
            if (markers[i] == null)
                continue;

            markerMaterials[i] = markers[i].material;
            originalColors[i] = GetMaterialColor(markerMaterials[i]);
            originalPositions[i] = markers[i].transform.localPosition;
        }

        if (threadAStatusLight != null)
            statusMaterial = threadAStatusLight.material;
    }

    private void Start()
    {
        ResetSimulation();
    }

    public void NextStep()
    {
        switch (currentState)
        {
            case ThreadState.New:
                SetState(ThreadState.Ready);
                break;

            case ThreadState.Ready:
                SetState(ThreadState.Running);
                break;

            case ThreadState.Running:
                SetState(ThreadState.Terminated);
                break;

            case ThreadState.Waiting:
                SetState(ThreadState.Ready);
                break;

            case ThreadState.Terminated:
                Debug.Log("Thread A has already finished.");
                break;
        }
    }

    public void ResetSimulation()
    {
        SetState(ThreadState.New);
    }

    private void SetState(ThreadState nextState)
    {
        currentState = nextState;
        Module1CodeHighlighter highlighter =
    FindFirstObjectByType<Module1CodeHighlighter>();

if (highlighter != null)
    highlighter.ShowState(currentState);

        if (animationRoutine != null)
            StopCoroutine(animationRoutine);

        animationRoutine = StartCoroutine(AnimateState());

        string message = "Thread A: " + currentState;

        if (statusText != null)
            statusText.text = message;

        Debug.Log(message);
    }

    private IEnumerator AnimateState()
    {
        int activeIndex = GetStateIndex(currentState);

        Vector3[] startPositions = new Vector3[markers.Length];
        Color[] startColors = new Color[markers.Length];

        for (int i = 0; i < markers.Length; i++)
        {
            if (markers[i] == null)
                continue;

            startPositions[i] = markers[i].transform.localPosition;
            startColors[i] = GetMaterialColor(markerMaterials[i]);
        }

        Color startLightColor =
            statusMaterial != null
            ? GetMaterialColor(statusMaterial)
            : Color.white;

        Color targetLightColor = GetStateColor(currentState);

        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, animationDuration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.SmoothStep(
                0f, 1f, Mathf.Clamp01(elapsed / duration)
            );

            for (int i = 0; i < markers.Length; i++)
            {
                if (markers[i] == null)
                    continue;

                Vector3 targetPosition = originalPositions[i];

                if (i == activeIndex)
                    targetPosition += Vector3.up * raisedHeight;

                markers[i].transform.localPosition =
                    Vector3.Lerp(
                        startPositions[i],
                        targetPosition,
                        t
                    );

                Color targetColor = originalColors[i];

                if (i == activeIndex)
                    targetColor *= brightness;

                SetMaterialColor(
                    markerMaterials[i],
                    Color.Lerp(startColors[i], targetColor, t)
                );
            }

            if (statusMaterial != null)
            {
                SetMaterialColor(
                    statusMaterial,
                    Color.Lerp(
                        startLightColor,
                        targetLightColor,
                        t
                    )
                );
            }

            yield return null;
        }

        // Apply exact final values.
        for (int i = 0; i < markers.Length; i++)
        {
            if (markers[i] == null)
                continue;

            Vector3 target = originalPositions[i];

            if (i == activeIndex)
                target += Vector3.up * raisedHeight;

            markers[i].transform.localPosition = target;

            Color targetColor = originalColors[i];

            if (i == activeIndex)
                targetColor *= brightness;

            SetMaterialColor(markerMaterials[i], targetColor);
        }

        if (statusMaterial != null)
            SetMaterialColor(statusMaterial, targetLightColor);

        animationRoutine = null;
    }

    private int GetStateIndex(ThreadState state)
    {
        switch (state)
        {
            case ThreadState.New: return 0;
            case ThreadState.Ready: return 1;
            case ThreadState.Running: return 2;
            case ThreadState.Terminated: return 3;
            default: return -1;
        }
    }

    private Color GetStateColor(ThreadState state)
    {
        switch (state)
        {
            case ThreadState.New: return newColor;
            case ThreadState.Ready: return readyColor;
            case ThreadState.Running: return runningColor;
            case ThreadState.Waiting: return waitingColor;
            case ThreadState.Terminated: return terminatedColor;
            default: return Color.white;
        }
    }

    private Color GetMaterialColor(Material mat)
    {
        if (mat.HasProperty("_BaseColor"))
            return mat.GetColor("_BaseColor");

        if (mat.HasProperty("_Color"))
            return mat.GetColor("_Color");

        return Color.white;
    }

    private void SetMaterialColor(Material mat, Color color)
    {
        if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", color);
        else if (mat.HasProperty("_Color"))
            mat.SetColor("_Color", color);
    }
}
