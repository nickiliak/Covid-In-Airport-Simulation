using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarSimulation : MonoBehaviour
{
    public GameObject Target;
    public float RotationYAxis;

    public GameObject[] CarPrefabs;
    public List<GameObject> Cars;
    public void SpawnCarPeriodically()
    {
        int index = Random.Range(0, CarPrefabs.Length);

        if(CarPrefabs[index].GetComponent<NavMeshAgent>() == null)
            CarPrefabs[index].AddComponent<NavMeshAgent>();

        GameObject Car = Instantiate(CarPrefabs[index], transform.position + new Vector3(0, 0.6f, 0), Quaternion.Euler(0, RotationYAxis, 0));

        Car.GetComponent<NavMeshAgent>().destination = Target.transform.position;
        Cars.Add(Car);

        Car.GetComponent<NavMeshAgent>().speed = Random.Range(8, 12);
        Car.GetComponent<NavMeshAgent>().updateRotation = false;
        Car.GetComponent<NavMeshAgent>().radius = 0.1f;
        Invoke(nameof(SpawnCarPeriodically), Random.Range(3,8));
    }
    private void Start()
    {
        SpawnCarPeriodically();
    }

    private void FixedUpdate()
    {
        for (int i = Cars.Count - 1; i >= 0; i--)
        {
            GameObject car = Cars[i];

            if (car.GetComponent<NavMeshAgent>().hasPath == true && car.GetComponent<NavMeshAgent>().remainingDistance < 2)
            {
                Cars.Remove(car);
                Destroy(car);
            }
        }
    }
}
