using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Rigidbody cameraRigidBody;
    public float moveForce = 750.0f;
    public float forwardForce = 150.0f;
    public FixedJoystick joystick;

    private float lastForceX = 0;
    private float lastForceY = 0;
    private float newForceX = 0;
    private float newForceY = 0;


    private void Awake()
    {
        cameraRigidBody = GetComponent<Rigidbody>();
        //joystick = GetComponent<FixedJoystick>();
    }
    void MoveFoward()
    {
        this.cameraRigidBody.AddForce(0, 0, forwardForce * 10, ForceMode.Force);
    }

    void MoveAround()
    {
        float localForce = 0;
        if(Input.anyKey)
        {
            localForce = 1;
        }
        else
        {
            this.cameraRigidBody.AddForce((-1) * lastForceX, (-1) * lastForceY, 0, ForceMode.Acceleration);
            //newForceX = 0;
            //newForceY = 0;
        }
        //Debug.Log(joystick.Vertical);
        //Debug.Log(joystick.Horizontal);

        if (joystick.Vertical != 0.0f)
        {
            Debug.Log("VERTICAL");
            this.cameraRigidBody.AddForce(0, moveForce * joystick.Vertical, 0, ForceMode.Acceleration);

        }

        if (joystick.Horizontal != 0.0f)
        {
            Debug.Log("HORIZONTAL");

            this.cameraRigidBody.AddForce(moveForce * joystick.Horizontal, 0, 0, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.cameraRigidBody.AddForce(0, moveForce * localForce, 0, ForceMode.Acceleration);
            //newForceY += moveForce;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.cameraRigidBody.AddForce(0, -moveForce * localForce, 0, ForceMode.Acceleration);
            //newForceY += -moveForce;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.cameraRigidBody.AddForce(-moveForce * localForce, 0, 0, ForceMode.Acceleration);
            //newForceX += -moveForce;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.cameraRigidBody.AddForce(moveForce * localForce, 0, 0, ForceMode.Acceleration);
            //newForceX += moveForce;

        }

        if (Input.GetKey(KeyCode.S))
        {
            this.cameraRigidBody.AddForce(-this.cameraRigidBody.velocity.x, -this.cameraRigidBody.velocity.y, -this.cameraRigidBody.velocity.z, ForceMode.Acceleration);
        }

        //lastForceX = newForceX;
        //lastForceY = newForceY;

    }
    void FixedUpdate()
    {
        MoveFoward();
        MoveAround();
    }
}
