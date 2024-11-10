using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SceneMan : MonoBehaviour
{
    public Transform player;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI friendsText;
    public bool isGamePaused = false;
    public GameObject sceneManager;

    private bool isInBsmnt = false;
    private Player_Script playerScript;
    private MenuScripts menuScripts;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (playerScript.friendsSaved == 3 || playerScript.lives == 0)
        {
            //Debug.Log("Game Over was called");
            gameOverMenu.SetActive(true);
            friendsText.text = "Friends Saved: " + playerScript.friendsSaved;
        }
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

    public void CallPause()
    {
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
}
