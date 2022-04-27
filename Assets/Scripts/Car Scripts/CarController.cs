using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody sRigidBody;
    [SerializeField] private float forwardAccel = 10f, maxAccel = 10f, turnStrength = 100f, gravityForce = 10f, dragOnGround = 3f;
    [SerializeField] private float speedMulti = 500f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private int maxTouchCount;
    [SerializeField] private ParticleSystem[] tireSmokes;

    private float turnInput;
    private bool grounded;
    private bool tirePlaying = false;
    private Vector3 oldPos;

    public float totalDistance = 0;
    public bool record = true;

    [SerializeField] TextMeshProUGUI mphDisplay;
    [SerializeField] TextMeshProUGUI distanceTravelled;
    

    private void OnEnable()
    {
        EventManager.onFuelPickup += FuelPickup;
        EventManager.onFuelEmpty += EmptyFuel;
    }

    private void OnDisable()
    {
        EventManager.onFuelEmpty -= EmptyFuel;
        EventManager.onFuelEmpty -= EmptyFuel;
    }
    private void Start()
    {
        sRigidBody.transform.parent = null;
        oldPos = transform.position;
        float maxAccel = forwardAccel;
    }
    private void Update()
    {
        if (forwardAccel >= 0.01f)
        {
            foreach (var item in tireSmokes)
            {
                if (!item.isPlaying)
                    item.Play();
            }

            turnInput = 0;
            var touchCount = Input.touchCount;
            if (touchCount > maxTouchCount)
            {
                touchCount = maxTouchCount;
            }
            for (int i = 0; i < touchCount; i++)
            {
                var t = Input.GetTouch(i);
                if (t.position.x < Screen.width / 2)
                {
                    turnInput = -1;
                }
                else if (t.position.x > Screen.width / 2)
                {
                    turnInput = 1;
                }
            }


            if (grounded)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
            }
            transform.position = sRigidBody.transform.position;
        }
        else
        {
            for (int i = 0; i < tireSmokes.Length; i++)
            {
                if (tireSmokes[i].isPlaying)
                {
                    tireSmokes[i].Stop();
                }
      
            }

            //end game loop
            FindObjectOfType<GameManager>().EndGame();
            
        }

        int mph = Mathf.FloorToInt(sRigidBody.velocity.magnitude * 2.237f);
        mphDisplay.text = mph.ToString() + "mph";
        distanceTravelled.text = Mathf.FloorToInt(totalDistance).ToString() + "m";

    }

    private void FixedUpdate()
    {
        grounded = false;
#if UNITY_EDITOR
        Debug.DrawLine(transform.localPosition, transform.localPosition + (-transform.up * groundRayLength), Color.red, 0.2f);
#endif

        if (Physics.Raycast(transform.position, -transform.up, out var hit, groundRayLength, ground))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        
        if (grounded)
        {
            sRigidBody.drag = dragOnGround;
            sRigidBody.AddForce(transform.forward * (forwardAccel * speedMulti));

            if (sRigidBody.velocity.sqrMagnitude > forwardAccel)
            {
                sRigidBody.velocity *= 0.99f;
            }

            EnableTireSmoke();
        }
        else
        {
            DisableTireSmoke();
            sRigidBody.drag = 0.1f;
            sRigidBody.AddForce(Vector3.up * (-gravityForce * 100f));
            
        }

        if (record)
        {
            RecordDistance();
        }

    }

    private void FuelPickup()
    {
        forwardAccel = maxAccel;
    }

    private void EmptyFuel()
    {
        forwardAccel -= Time.deltaTime * 1.5f;
        forwardAccel = Mathf.Clamp(forwardAccel, 0, 50);

        turnStrength -= Time.deltaTime * 2f;
        turnStrength = Mathf.Clamp(turnStrength, 0, 1000);
    }

    private void EnableTireSmoke()
    {
        if (tirePlaying) return;
        
        foreach (var item in tireSmokes)
        {
            item.Play();   
        }

        tirePlaying = true;
    }
    private void DisableTireSmoke()
    {
        if (!tirePlaying) return;
       
        foreach (var item in tireSmokes)
        {
            item.Stop();   
        }

        tirePlaying = false;
    }
    private void RecordDistance()
    {
        totalDistance += Vector3.Distance(transform.position, oldPos);
        oldPos = transform.position;
    }
}