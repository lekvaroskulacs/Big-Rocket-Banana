using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
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
        movement.processInputs = false;
        Invoke("ReloadCurrentScene", 1);
    }

    private void ReloadCurrentScene()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        movement.processInputs = true;
    }

    private void LandingPad()
    {
        movement.processInputs = false;
        Invoke("LoadNextScene", 1);
    }

    private void LoadNextScene()
    {
        if (SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 1)
        {

            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        movement.processInputs = true;
    }

    private void LaunchPad()
    {
    }
}
