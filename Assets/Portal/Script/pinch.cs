using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion;
using ManoMotion.RunTime;


public class Pinch : MonoBehaviour
{
    private bool isPinching;

    // Reference to the water element
    public GameObject MushroomLarge;

    void Start()
    {
        // If MushroomLarge is not assigned in the Inspector, find it by tag
        if (MushroomLarge == null)
        {
            MushroomLarge = GameObject.FindGameObjectWithTag("WaterElement");
        }

        // Initialization is assumed to be handled by the ManoMotion Manager elsewhere
    }

    void Update()
    {
        // Get the gesture information from ManoMotion
        HandInfo handInfo = ManomotionManager.Instance.Hand_infos[0].hand_info;
        GestureInfo gestureInfo = handInfo.gesture_info;

        // Assuming 'PICK' is the gesture trigger for pinch (replace with actual gesture trigger from SDK)
        if (gestureInfo.mano_gesture_trigger == ManoGestureTrigger.PICK)
        {
            if (!isPinching)
            {
                PinchIn();
                isPinching = true;
            }
        }
        else
        {
            if (isPinching)
            {
                PinchOut();
                isPinching = false;
            }
        }
    }

    void PinchIn()
    {
        Debug.Log("Pinch In");

        if (MushroomLarge != null)
        {
            // Example: Scale down the water element
            MushroomLarge.transform.localScale *= 0.9f;

            // Additional logic: Adjusting transparency
            Renderer renderer = MushroomLarge.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = Mathf.Clamp(color.a * 0.9f, 0, 1);
                renderer.material.color = color;
            }
        }
    }

    void PinchOut()
    {
        Debug.Log("Pinch Out");

        if (MushroomLarge != null)
        {
            // Example: Scale up the water element
            MushroomLarge.transform.localScale *= 1.1f;

            // Additional logic: Adjusting transparency
            Renderer renderer = MushroomLarge.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = Mathf.Clamp(color.a * 1.1f, 0, 1);
                renderer.material.color = color;
            }
        }
    }
}
