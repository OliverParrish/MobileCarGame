using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public float Strength;
    public float Speed;

    void Start()
    {
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Loops forever
        while (true)
        {
            float timer = 0f;
            float originalSize = transform.localScale.x;

            // Heart beat twice
            for (int i = 0; i < 2; i++)
            {
                // Zoom in
                while (timer < 0.1f)
                {
                    yield return new WaitForEndOfFrame();
                    timer += Time.deltaTime;

                    transform.localScale = new Vector3
                    (
                        transform.localScale.x + (Time.deltaTime * Strength * 2),
                        transform.localScale.y + (Time.deltaTime * Strength * 2)
                    );
                }
            }

            // Return to normal
            while (transform.localScale.x < originalSize)
            {
                yield return new WaitForEndOfFrame();

                transform.localScale = new Vector3
                (
                    transform.localScale.x - Time.deltaTime * Strength,
                    transform.localScale.y - Time.deltaTime * Strength
                );
            }

            transform.localScale = new Vector3(originalSize, originalSize);

            yield return new WaitForSeconds(Speed);
        }
    }
    
}
