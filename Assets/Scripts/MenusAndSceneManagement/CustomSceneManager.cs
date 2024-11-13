using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CustomSceneManager : MonoBehaviour
{
    // Static reference to the instance of our SceneManager
    public static CustomSceneManager instance;

    private GameObject sceneMan;
    private SceneMan sceneManScript;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this object to persist across scene loads
        DontDestroyOnLoad(gameObject);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        // Re-subscribe to sceneLoaded in case it was unsubscribed
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid potential memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This is called every time a scene is loaded

        // Find the SceneManager GameObject and assign the SceneMan script if it exists
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            sceneMan = GameObject.Find("SceneManager");

            if (sceneMan != null)
            {
                sceneManScript = sceneMan.GetComponent<SceneMan>();
                if (sceneManScript == null)
                {
                    Debug.LogError("SceneMan script is not attached.");
                }
            }
            else
            {
                Debug.LogWarning("SceneManager GameObject not found in the new scene.");
            }
        }
    }

    private void Update()
    {
        // Make sure sceneManScript is assigned before trying to call CallPause
        if (sceneManScript != null && SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManScript.CallPause();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}