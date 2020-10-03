using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float zPosition = 0;
    private Rigidbody cameraRigidBody;
    float acceleration = 1;

    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float forceScale = 15.0f;
    private void Awake()
    {
        cameraRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        zPosition = 0;
    }

    void MoveFoward()
    {
        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = cameraRigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        cameraRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);

    }

    void MoveAround()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
        }
    }

    void Update()
    {
        //MoveFoward();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.cameraRigidBody.AddForce(0, 0, forceScale*50, ForceMode.Force);

        if (Input.GetKey(KeyCode.UpArrow))
        {
        this.cameraRigidBody.AddForce(0, forceScale*50, 0, ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.cameraRigidBody.AddForce(0, -forceScale*50, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        this.cameraRigidBody.AddForce(-forceScale*50, 0, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
        this.cameraRigidBody.AddForce(forceScale*50, 0, 0, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //this.cameraRigidBody.velocity = Vector3.zero;
        this.cameraRigidBody.AddForce(-this.cameraRigidBody.velocity.x, -this.cameraRigidBody.velocity.y, -this.cameraRigidBody.velocity.z, ForceMode.Acceleration);
            
        }
        //cameraRigidBody.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime);
        //transform.Translate(Input.acceleration.x, -Input.acceleration.y, );
    }
}
