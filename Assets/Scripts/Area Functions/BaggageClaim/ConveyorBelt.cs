using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 0.01f; // Adjust the speed as desired
    public Vector3 direction = Vector3.right;

    private void OnTriggerStay(Collider other)
    {

        Transform Trans = other.GetComponent<Transform>();
        if (Trans != null && other.tag == "SuitCase")
        {
            Trans.position = Trans.position + speed*direction;
        }
    }
}
