using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    [SerializeField] private float rightRotSpeed;
    [SerializeField] private float leftRotSpeed;
    [SerializeField] private float boostAmount;

    private Rigidbody rigidBody;
    private RocketSFXHandler sfx;

    private bool rotatingLeft = false;
    private bool rotatingRight = false;
    private bool boost = false;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        sfx = GetComponent<RocketSFXHandler>();
   
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
        }
        else
        {
            boost = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
            sfx.thrustEnableSFX();
        if (Input.GetKeyUp(KeyCode.W)) 
            sfx.thrustDisableSFX();
    }

    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotatingLeft = true;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotatingRight = true;
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

    private void LateUpdate()
    {
        rotatingLeft = false;
        rotatingRight = false;
    }

    public bool IsThrusting()
    {
        return boost;
    }

    public void OnDisable()
    {
        sfx.thrustDisableSFX();
    }

}
