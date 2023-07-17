using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Testing : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        AgentData collidedObjectData = collision.gameObject.GetComponent<AgentData>();

        if (collidedObjectData != null && Time.time - collidedObjectData.HeatmapTimer > 0.5f)
        {
            collidedObjectData.HeatmapTimer = Time.time;
            StartCoroutine(AddThenRemove(collidedObjectData, collision.transform.position, 0f));
       
        }

    }
    private IEnumerator AddThenRemove(AgentData collidedObjectData, Vector3 position, float delaySeconds)
    {
        HeatmapController HmC = GetComponent<HeatmapController>();
        
        // Add item to list
        HmC.Points.Add(position);

        yield return new WaitForSeconds(delaySeconds);

        // Delete item from the list
        // HmC.Points.Remove(position);


    }
}
