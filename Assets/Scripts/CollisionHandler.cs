using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] private float sceneLoadDelay = 1f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 100f;
    [SerializeField] private float upwardsModifier = 50f;
    
    Movement movement;
    RocketSFXHandler sfx;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] List<GameObject> rocketParts = new List<GameObject>();

    bool isTransitioning = false;
    bool collisionsEnabled = true;

    private void Start()
    {
        movement = GetComponent<Movement>();
        sfx = GetComponent<RocketSFXHandler>();
        
        
    }

    private void Update()
    {
        ProcessDebugKeys();
    }

    private void ProcessDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsEnabled = !collisionsEnabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collisionsEnabled)
            return; 

        if (isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Lethal":
                Die();
                break;
            case "LandingPad":
                LandingPad();
                break;
            case "LaunchPad":
                LaunchPad();
                break;


        }
    }

    private void Die()
    {
        isTransitioning = true;
        movement.enabled = false;
        explosionParticles.Play();

        sfx.explosionSFX();
        sfx.enabled = false;

        OnDeathRocketParts();
        Invoke("ReloadCurrentScene", sceneLoadDelay);
    }

    private void ReloadCurrentScene()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDeathRocketParts()
    {
        foreach(var part in rocketParts)
        {
            part.AddComponent<Rigidbody>();
            var rb = part.GetComponent<Rigidbody>();
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
        }
    }

    private void LandingPad()
    {
        isTransitioning = true;
        movement.enabled = false;
        successParticles.Play();

        sfx.successSFX();
        sfx.enabled = false;



        Invoke("LoadNextScene", sceneLoadDelay);
    }

    private void LoadNextScene()
    {
        if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void LaunchPad()
    {
    }
}
