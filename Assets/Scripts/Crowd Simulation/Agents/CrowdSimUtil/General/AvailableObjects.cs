using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableObjects : MonoBehaviour
{
    public List<GameObject> availableObjects;
    public List<GameObject> unavailableObjects;

    List<GameObject> GetChildObjectsByNameRecursive(Transform parent, string[] names)
    {
        List<GameObject> objectsWithName = new List<GameObject>();

        foreach (Transform child in parent)
        {
            foreach (string name in names)
                if (child.name == name)
                {
                    objectsWithName.Add(child.gameObject);
                }

            // If you want to search for objects in nested children as well, you can use recursion
            List<GameObject> nestedObjects = GetChildObjectsByNameRecursive(child, names);
            objectsWithName.AddRange(nestedObjects);
        }

        return objectsWithName;
    }

    public GameObject PickRandomAvailableObject()
    {
        if (availableObjects.Count == 0)
        {
            int RandomIndex = Random.Range(0, unavailableObjects.Count);
            GameObject gameObject = unavailableObjects[RandomIndex];
            return gameObject;
        }
        else
        {
            int RandomIndex = Random.Range(0, availableObjects.Count);
            GameObject gameObject = availableObjects[RandomIndex];
            unavailableObjects.Add(availableObjects[RandomIndex]);
            availableObjects.RemoveAt(RandomIndex);
            return gameObject;
        }
    }
    private void Start()
    {
        availableObjects = new List<GameObject>();
        unavailableObjects = new List<GameObject>();

        string[] names = new string[]
        {
            "Chair (0)",
            "Chair (1)",
            "Chair (2)",
            "Chair (3)",
            "Chair (4)"
        };


        availableObjects = GetChildObjectsByNameRecursive(gameObject.transform, names);
    }
}
