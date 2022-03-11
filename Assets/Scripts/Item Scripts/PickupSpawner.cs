using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform[] spawns;

    private void Start()
    {
        SpawnObjects(objectsToSpawn, spawns);
    }

    private void SpawnObjects(GameObject[] gameObjects, Transform[] locations, bool allowOverlap = false)
    {
        List<GameObject> remainingGameObjects = new List<GameObject>(gameObjects);
        List<Transform> freeLocations = new List<Transform>(locations);

        if (locations.Length < gameObjects.Length)
        {
            Debug.LogWarning(allowOverlap? "there are more gameObjects than locations. some objects will overlap" : "There are not enough locations for all the gameObjects." +
                " Some won't spawn");
        }
        while (remainingGameObjects.Count > 0)
        {
            if (freeLocations.Count == 0)
            {
                if (allowOverlap)
                {
                    freeLocations.AddRange(locations);
                }
                else break;
            }

            int gameObjectIndex = Random.Range(0, remainingGameObjects.Count);
            int locationIndex = Random.Range(0, freeLocations.Count);

            if (Random.value <= 0.7f)
            {
                var fuelPickup = Instantiate(gameObjects[gameObjectIndex], locations[locationIndex].position, Quaternion.identity);
                fuelPickup.transform.SetParent(gameObject.transform);
            }
            remainingGameObjects.RemoveAt(gameObjectIndex);
            freeLocations.RemoveAt(locationIndex);
        }
        
    }
}
