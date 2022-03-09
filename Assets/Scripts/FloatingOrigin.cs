using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticle;
    [SerializeField] private ParticleSystem smokeParticle2;
    [SerializeField] private Transform car;
    [SerializeField] private float threshold = 100.0f;
    [SerializeField] private LevelLayoutGenerator layoutGenerator;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;

        if (cameraPosition.magnitude > threshold)
        {
            smokeParticle.Stop();
            smokeParticle2.Stop();

            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (go.name == "CM vcam1")
                    continue;
                go.transform.position -= cameraPosition;
            }

            smokeParticle.Play();
            smokeParticle2.Play();

            Vector3 originDelta = Vector3.zero - cameraPosition;
            cinemachineCamera.OnTargetObjectWarped(car, originDelta);
            layoutGenerator.UpdateSpawnOrigin(originDelta);
#if UNITY_EDITOR
            Debug.unityLogger.Log("Recentering, origin delta = " + originDelta);
#endif
        }
    }

    
}
