using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Testing : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        GenerateAgentAttributes collidedObjectData = collision.gameObject.GetComponent<GenerateAgentAttributes>();

        if (collidedObjectData != null && Time.time - collidedObjectData.HeatmapTimer > 1f)
        {
            collidedObjectData.HeatmapTimer = Time.time;
            HeatmapController HmC = GetComponent<HeatmapController>();
            HmC.Points.Add(collision.transform.position);
        }

    }
}
