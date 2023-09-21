using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    public Transform player;
    public Transform exitDoor;
    public Camera mainCamera;
    public float arrowVisibleThreshold = 1.0f; // Adjust this value as needed.

    private RectTransform arrowRectTransform;

    private void Start()
    {
        arrowRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (player != null && exitDoor != null && levelDesign.Instance.getScore() >= levelDesign.Instance.BossTime)
        {
            
            // Calculate the direction from the player to the exit door.
            Vector3 directionToExit = exitDoor.position - player.position;
            directionToExit.z = 0; // Ensure it's in 2D space.

            // Check if the exit door is within the camera's view.
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(exitDoor.position);

            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1 || viewportPoint.z < 0)
            {
                // The exit door is outside the camera's view.
                // Calculate the rotation angle for the arrow.
                float angle = Mathf.Atan2(directionToExit.y, directionToExit.x) * Mathf.Rad2Deg;

                angle += 180f;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // Rotate the arrow to point in the direction of the exit door.
                arrowRectTransform.rotation = rotation;

                // Make the arrow visible.
                arrowRectTransform.gameObject.SetActive(true);
            }
            else
            {
                // The exit door is within the camera's view.
                // Hide the arrow.
                arrowRectTransform.gameObject.SetActive(false);
            }
        }
    }
}
