using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 0.01f; // Adjust the speed as desired
    public Vector3 direction = Vector3.right;

    private void OnCollisionStay(Collision collision)
    {
        Transform Trans = collision.gameObject.GetComponent<Transform>();
        if (Trans != null && collision.gameObject.tag == "SuitCase")
        {
            Trans.position = Trans.position + speed * direction;
        }
    }
}
