using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody sRigidBody;
    [SerializeField] private float forwardAccel = 10f, maxSpeed = 50f, turnStrength = 100f, gravityForce = 10f, dragOnGround = 3f;
    [SerializeField] private float speedMulti = 500f;
    private float speedInput, turnInput;

    private bool grounded;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundRayLength = 0.5f;

    [SerializeField] private int maxTouchCount;

    // Start is called before the first frame update
    void Start()
    {
        sRigidBody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
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
        
        speedInput = 0f;

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
        }

        transform.position = sRigidBody.transform.position;
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
            sRigidBody.AddForce(transform.forward * forwardAccel * speedMulti);

            if (sRigidBody.velocity.sqrMagnitude > maxSpeed)
            {
                sRigidBody.velocity *= 0.99f;
            }

        }
        else
        {
            sRigidBody.drag = 0.1f;
            sRigidBody.AddForce(Vector3.up * -gravityForce * 100f);
        }
        
    }
}