using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            EventManager.FuelPickup();
            Destroy(gameObject);
        } 
    }
}