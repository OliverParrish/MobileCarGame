using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;
    
    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Car")) return;
        OnChunkExited();
        StartCoroutine(WaitAndDeactivate());
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        transform.root.gameObject.SetActive(false);

    }



}
