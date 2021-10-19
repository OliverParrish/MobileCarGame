using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Capsule")
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
