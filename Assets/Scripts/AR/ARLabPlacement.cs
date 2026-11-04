
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
    private readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool HasPlacedLab => placedLab != null;

    void Update()
    {
        if (HasPlacedLab) return;

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

           placedLab = Instantiate(
                labPrefab,
                pose.position,
                Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f)
            );

            placedLab.SetActive(true);

            Debug.Log("Multithreading Lab placed!");
        }
    }

    bool TryGetPointerPress(out Vector2 position)
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
