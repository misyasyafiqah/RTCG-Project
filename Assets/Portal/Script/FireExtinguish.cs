using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}, Tag: {other.gameObject.tag}");

        if (other.CompareTag("Fire"))
        {
            ExtinguishFire(other.gameObject);
        }
    }

    void ExtinguishFire(GameObject fireObject)
    {
        Debug.Log($"Extinguishing Fire: {fireObject.name}");
        Destroy(fireObject);
    }
}
