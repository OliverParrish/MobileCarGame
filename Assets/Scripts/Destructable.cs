using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("CarCollider"))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
