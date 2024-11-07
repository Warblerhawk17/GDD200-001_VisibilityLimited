using UnityEditor.Build.Content;
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
    public int sceneIndex = 0;
    public Transform player;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI friendsText;

    private bool isGamePaused = false;
    private bool isInBsmnt = false;
    private Player_Script playerScript;

    private void Start()
    {
        if (pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
        }

        if (gameOverMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
        }

            playerScript = player.GetComponent<Player_Script>();
    }
    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // If instance already exists and it's not this, then destroy this to enforce the singleton.
            Destroy(gameObject);
        }

        // Set this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    //Pause menu method
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape responded correctly");
            if (pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
                isGamePaused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                isGamePaused = false;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex != 0 && (playerScript.friendsSaved == 3 || playerScript.lives == 0))
        {
            Debug.Log("Game Over was called");
            gameOverMenu.SetActive(true);
            friendsText.text = "Friends Saved: " + playerScript.friendsSaved;
        }
    }

    // General method to load scenes based on build index
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger activated");

        // Check if the player is the object entering the trigger
        if (collision.CompareTag("Player"))
        {
            if (player.position.y > -10)
            {
                player.transform.position = new Vector2(player.position.x, -21.5f);
            } 
            else if (player.position.y < -10)
            {
                player.transform.position = new Vector2(player.position.x, 3.8f);
            }
        }
    }
}

