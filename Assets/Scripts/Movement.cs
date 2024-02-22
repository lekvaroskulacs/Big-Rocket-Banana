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
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

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

    }

    private void ProcessBoostIntrinsicEffects()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sfx.thrustEnableSFX();
            mainThrustParticles.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            sfx.thrustDisableSFX();
            mainThrustParticles.Stop();
        }
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

        ProcessBoostIntrinsicEffects();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        if (rotatingLeft)
        {
            RotateRocketInDirection(Direction.left);
        }

        if (rotatingRight)
        {
            RotateRocketInDirection(Direction.right);
        }

        RotationParticles();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        
    }

    private void RotationParticles()
    {
        if (rotatingLeft)
        {
            if (!rightThrustParticles.isPlaying)
                rightThrustParticles.Play();
        }
        else if (rotatingRight)
        {
            if (!leftThrustParticles.isPlaying)
                leftThrustParticles.Play();
        }
        else
        {
            if (rightThrustParticles.isPlaying)
                rightThrustParticles.Stop();
            if (leftThrustParticles.isPlaying)
                leftThrustParticles.Stop();
        }
    }

    private enum Direction { left, right };

    private void RotateRocketInDirection(Direction dir)
    {
        float signum;
        float rotSpeed;
        if (dir == Direction.left)
        {
            signum = 1;
            rotSpeed = leftRotSpeed;
        }
        else
        {
            signum = -1;
            rotSpeed = rightRotSpeed;
        }
        

        transform.Rotate(new Vector3(0, 0, signum * rotSpeed * Time.deltaTime));
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
        sfx?.thrustDisableSFX();
    }



}
