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

    public delegate void ThrustDelegate();

    public event ThrustDelegate thrustEnable;
    public event ThrustDelegate thrustDisable;

    private bool processInputsValue;
    public bool processInputs
    {
        get
        {
            return processInputsValue;
        } 
        set
        {
            processInputsValue = value;
            boost = false;
            rotatingLeft = false;
            rotatingRight = false;
            thrustDisable?.Invoke();
        }
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        processInputs = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (processInputs)
        {
            ProcessBoost();
            ProcessRotate();
        }

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
            thrustEnable?.Invoke();
        if (Input.GetKeyUp(KeyCode.W)) 
            thrustDisable?.Invoke();
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

}
