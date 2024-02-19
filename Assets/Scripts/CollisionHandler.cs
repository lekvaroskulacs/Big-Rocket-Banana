using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] private float sceneLoadDelay = 1f;
    
    Movement movement;
    RocketSFXHandler sfx;


    private void Start()
    {
        movement = GetComponent<Movement>();
        sfx = GetComponent<RocketSFXHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        movement.enabled = false;

        sfx.explosionSFX();

        Invoke("ReloadCurrentScene", sceneLoadDelay);
    }

    private void ReloadCurrentScene()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LandingPad()
    {
        movement.enabled = false;

        sfx.successSFX();

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
