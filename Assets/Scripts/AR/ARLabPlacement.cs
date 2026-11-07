
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARLabPlacement : MonoBehaviour
{
    [Header("AR References")]
    public ARRaycastManager raycastManager;
    public GameObject labPrefab;

    private GameObject placedLab;

    private readonly List<ARRaycastHit> hits =
        new List<ARRaycastHit>();

    private bool placementAllowed = false;
    private int selectedModule = 0;

    public bool HasPlacedLab => placedLab != null;

    public void EnablePlacement(int moduleNumber)
    {
        DisablePlacement();

        selectedModule = moduleNumber;
        placementAllowed = true;

        Debug.Log(
            "Module " + selectedModule +
            " selected. Tap AR plane to place lab."
        );
    }

    public void DisablePlacement()
    {
        placementAllowed = false;
        selectedModule = 0;

        if (placedLab != null)
        {
            Destroy(placedLab);
            placedLab = null;
        }

        Debug.Log("AR Lab removed.");
    }

    private void Update()
    {
        if (!placementAllowed || HasPlacedLab)
            return;

        if (!TryGetPointerPress(out Vector2 screenPosition))
            return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        if (raycastManager == null || labPrefab == null)
            return;

        if (raycastManager.Raycast(
            screenPosition,
            hits,
            TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            float cameraYRotation = 0f;

            if (Camera.main != null)
            {
                cameraYRotation =
                    Camera.main.transform.eulerAngles.y;
            }

            placedLab = Instantiate(
                labPrefab,
                pose.position,
                Quaternion.Euler(0f, cameraYRotation, 0f)
            );

            placedLab.SetActive(true);

            ConfigureSelectedModule();

            Debug.Log(
                "Module " + selectedModule +
                " placed successfully."
            );
        }
    }

    private void ConfigureSelectedModule()
    {
        if (placedLab == null)
            return;

        Transform module1 =
            placedLab.transform.Find("Module1_Lifecycle");

        Transform module2 =
            placedLab.transform.Find("Module2_Concurrent");

        Transform module3 =
            placedLab.transform.Find("Module3_RaceCondition");

        if (module1 != null)
            module1.gameObject.SetActive(selectedModule == 1);

        if (module2 != null)
            module2.gameObject.SetActive(selectedModule == 2);

        if (module3 != null)
            module3.gameObject.SetActive(selectedModule == 3);

        if (selectedModule == 1)
        {
            ThreadSimulationManager manager =
                placedLab.GetComponent<ThreadSimulationManager>();

            if (manager != null)
                manager.ResetSimulation();
        }
        else if (selectedModule == 2)
        {
            ConcurrentExecutionManager manager =
                placedLab.GetComponent<ConcurrentExecutionManager>();

            if (manager != null)
                manager.ResetSimulation();
        }

        else if (selectedModule == 3)
{
    RaceConditionSimulationManager manager =
        placedLab.GetComponent<RaceConditionSimulationManager>();

    if (manager != null)
        manager.ResetSimulation();
}
    }

    private bool TryGetPointerPress(out Vector2 position)
    {
        position = Vector2.zero;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            position =
                Touchscreen.current.primaryTouch.position.ReadValue();

            return true;
        }

#if UNITY_EDITOR
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            position = Mouse.current.position.ReadValue();

            return true;
        }
#endif

        return false;
    }
}
