using UnityEngine;

public class PointAndDragHandler : MonoBehaviour
{
    public GameObject cube;       // Reference to the cube GameObject

    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Ensure there is a camera tagged as 'MainCamera' in the scene.");
        }
    }

    void Update()
    {
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
        ManoClass manoClass = handInfo.gesture_info.mano_class;

        if (manoClass == ManoClass.POINTER_GESTURE)
        {
            Vector3 handPosition = handInfo.tracking_info.palm_center;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(handPosition);

            Debug.Log("Pointer gesture detected. Hand position: " + handPosition);

            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == cube)
                {
                    Debug.Log("Pointer detected on the cube.");

                    if (!isDragging)
                    {
                        isDragging = true;
                        Debug.Log("Started dragging cube");
                    }
                }
            }
        }
        else
        {
            if (isDragging)
            {
                isDragging = false;
                Debug.Log("Stopped dragging");
            }
        }
    }
}

