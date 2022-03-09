using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    [SerializeField] private float delay = 5f;
    
    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        OnChunkExited();
        DestroyAfterTime();
    }

    private void DestroyAfterTime()
    {
        Destroy(transform.root.gameObject, 5f);
    }
}
