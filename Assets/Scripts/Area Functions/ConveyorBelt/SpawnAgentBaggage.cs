using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgentBaggage : MonoBehaviour
{
    public GameObject Items;
    public GameObject SuitCase;

    GameObject InstantiatedObject;
    Vector3 offset = new Vector3(-0.4f, 0, 0.2f);

    public GameObject SpawnBaggage(string AgentID)
    {
        InstantiatedObject = Instantiate(SuitCase, transform.position + offset, Quaternion.identity);
        InstantiatedObject.transform.parent = Items.transform;

        string BaggageName = "Bag_" + AgentID;
        InstantiatedObject.name = BaggageName;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("SuitCaseLayer"), LayerMask.NameToLayer("SuitCaseLayer")); // NEED TO MOVE THIS WTF 
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("SuitCaseLayer"), LayerMask.NameToLayer("HeatMapLayer")); // NEED TO MOVE THIS WTF 

        Renderer renderer = InstantiatedObject.GetComponent<Renderer>();
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        renderer.material.color = randomColor;

        return InstantiatedObject;
    }
}
