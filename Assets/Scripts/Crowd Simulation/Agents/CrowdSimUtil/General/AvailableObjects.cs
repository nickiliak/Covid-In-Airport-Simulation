using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableObjects : MonoBehaviour
{
    public List<GameObject> availableObjects;
    public List<int> availablePositions;

    List<GameObject> GetChildObjectsByNameRecursive(Transform parent, string[] names)
    {
        List<GameObject> objectsWithName = new List<GameObject>();

        foreach (Transform child in parent)
        {
            foreach (string name in names)
                if (child.name == name)
                    objectsWithName.Add(child.gameObject);
                

            // If you want to search for objects in nested children as well, you can use recursion
            List<GameObject> nestedObjects = GetChildObjectsByNameRecursive(child, names);
            objectsWithName.AddRange(nestedObjects);
        }

        return objectsWithName;
    }

    public GameObject PickRandomAvailableObject()
    {
        Debug.Log(availableObjects.Count);
        if (availableObjects.Count == 0)
        {
            GameObject RPS = GameObject.Find("RandomPositions");

            int listPos = Random.Range(0, availablePositions.Count - 1);
            int PosNo = availablePositions[listPos];
            availablePositions.RemoveAt(listPos);

            GameObject RandomPosition = RPS.transform.Find("Pos (" + PosNo + ")").gameObject;
            return RandomPosition;
        }
        else
        {
            int RandomIndex = Random.Range(0, availableObjects.Count);
            GameObject gameObject = availableObjects[RandomIndex];
            availableObjects.RemoveAt(RandomIndex);
            return gameObject;
        }
    }
    private void Start()
    {
        availableObjects = new List<GameObject>();
        availablePositions = new List<int>();

        for (int i = 0; i < GameObject.Find("RandomPositions").transform.childCount - 1; i++)
            availablePositions.Add(i);

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
