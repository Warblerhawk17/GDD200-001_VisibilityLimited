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
    public GameObject sceneManager;
    
    private PlayerMovement playerMovement;
    private player_script playerScript;
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

        playerScript = player.GetComponent<player_script>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.friendsSaved == 4 || playerScript.lives == 0)
        {
            //Debug.Log("Game Over was called");
            gameOverMenu.SetActive(true);
            friendsText.text = "Friends Saved: " + playerScript.friendsSaved;
            playerMovement.canMove = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger activated");

        // Check if the player is the object entering the trigger
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.y > -10) //down
            {
                collision.transform.position = new Vector2(player.position.x, -21.5f);
            }
            else if (collision.transform.position.y < -10) //up
            {
                collision.transform.position = new Vector2(player.position.x, 3.8f);
            }
            for (int i = 0; i < playerScript.friendList.Count; i++) 
            {
                playerScript.friendList[i].transform.position=player.position;
            }
        }
    }

    public void CallPause()
    {
        if (pauseMenu.activeInHierarchy == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            playerMovement.canMove = false;
        }
        else if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            playerMovement.canMove = true;
        }
    }
}
