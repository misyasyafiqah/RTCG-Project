using System.Collections.Generic;
using UnityEngine;

public class CircleGestureDetector : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>(); // Stores fingertip positions
    private bool isTracking = false;
    public float tolerance = 0.1f; // Tolerance for circle detection


    public GameObject cubePrefab;

    // Update is called once per frame
    void Update()
    {
        // This part needs to be replaced with your specific method to detect an open hand gesture
        // and start/stop tracking based on your hand tracking setup, such as ManoMotion
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if (detectedHand.gesture_info.mano_gesture_continuous == ManoGestureContinuous.OPEN_HAND_GESTURE)
        {
            // Start tracking the center position of the hand when it's open
            isTracking = true;
            Vector3 handCenterPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                detectedHand.tracking_info.palm_center.x, 
                detectedHand.tracking_info.palm_center.y, 
                Camera.main.nearClipPlane + 0.5f)); // Adjust depth as needed
            positions.Add(handCenterPosition);
        }
        else
        {
            if (isTracking)
            {
                // Stop tracking and check for circle
                isTracking = false;
                if (IsCircle(positions, tolerance))
                {
                    Debug.Log("Circle gesture detected!");
                }
                positions.Clear();
            }
        }
    }

    bool IsCircle(List<Vector3> points, float tolerance)
    {
        if (points.Count < 20)  // Ensure enough points to form a reasonable circle
            return false;

        Vector3 centroid = CalculateCentroid(points);
        float averageRadius = CalculateAverageRadius(points, centroid);

        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(centroid, point);
            if (Mathf.Abs(distance - averageRadius) > tolerance)
            {
                return false; // If any point is outside the tolerance, it's not a circle
            }
        }

        return true; // All points are within tolerance of the average radius
    }

    Vector3 CalculateCentroid(List<Vector3> points)
    {
        Vector3 centroid = Vector3.zero;
        foreach (Vector3 point in points)
        {
            centroid += point;
        }
        centroid /= points.Count;
        return centroid;
    }

    float CalculateAverageRadius(List<Vector3> points, Vector3 centroid)
    {
        float totalDistance = 0;
        foreach (Vector3 point in points)
        {
            totalDistance += Vector3.Distance(centroid, point);
        }
        return totalDistance / points.Count;
    }
}