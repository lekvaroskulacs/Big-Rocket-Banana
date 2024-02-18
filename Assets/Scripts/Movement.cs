using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidBody;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool boost = false;

    [SerializeField] private float rightRotSpeed;
    [SerializeField] private float leftRotSpeed;
    [SerializeField] private float boostAmount;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessBoost();
        ProcessRotate();

        Thrust();
        Rotate();
    }

    void ProcessBoost()
    {    
        if (Input.GetKey(KeyCode.W))
        {
            boost = true;
            Debug.Log("boosting");
        }
        else
        {
            boost = false;
        }
    }

    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotatingLeft = true;
        }
        else
        {
            rotatingLeft = false;

            if (Input.GetKey(KeyCode.D))
            {
                rotatingRight = true;
            }
            else
            {
                rotatingRight = false;
            }
        }

        
    }

    private void Thrust()
    {
        Vector3 upThrust = new Vector3(0, 1, 0);
        if (boost)
        {
            rigidBody.AddRelativeForce(upThrust * boostAmount * Time.deltaTime);
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        if (rotatingLeft)
        {
            transform.Rotate(new Vector3(0, 0, leftRotSpeed * Time.deltaTime));
        }

        if (rotatingRight)
        {
            transform.Rotate(new Vector3(0, 0, -rightRotSpeed * Time.deltaTime));
        }

        rigidBody.freezeRotation = false;
    }

}
