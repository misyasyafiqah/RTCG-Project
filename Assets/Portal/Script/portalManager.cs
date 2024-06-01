using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    public Transform APos;
    public Transform BPos;

    public GameObject[] objectA;

    void Update()
    {
        objectA = GameObject.FindGameObjectsWithTag("APos");
        if (objectA.Length > 0)
        {
            APos = objectA[0].transform;
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Portal A"))
        {
            CharacterController cc = GetComponent<CharacterController>();

            cc.enabled = false;

            transform.position = BPos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, BPos.rotation.y, transform.rotation.z, transform.rotation.w);

            cc.enabled = true;
        }

        if (col.CompareTag("Portal B"))
        {
            CharacterController cc = GetComponent<CharacterController>();
            transform.position = APos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, APos.rotation.y, transform.rotation.z, transform.rotation.w);

            cc.enabled = true;
        }
    }
}
