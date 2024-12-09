using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScripts : MonoBehaviour
{

    public GameObject currentMenu;

    public Sprite im1;
    public Sprite im2;
    public AudioListener audioListener;
    public SceneMan sceneMan;

    private Button button;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MenuShowing);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void MenuShowing()
    {
        switch (index)
        {
            case 0:
                //from settings menu to main menu
                SceneManager.LoadScene(0);
                break;
            case 1:
                //from main menu to settings menu
                SceneManager.LoadScene(1);
                break;
            case 2:
                //Debug.Log("Resume button pressed");
                sceneMan.CallPause();
                break;
            case 3:
                Image buttonImage = GetComponent<Image>();

                if (buttonImage.sprite == im1)
                {
                    audioListener.enabled = false;
                    buttonImage.sprite = im2;
                }
                else
                {
                    audioListener.enabled = true;
                    buttonImage.sprite = im1;
                }
                break;
            case 4:
                //Debug.Log("Exit Game button pressed");
                Application.Quit();
                break;
            case 5:
                SceneManager.LoadScene(2);
                break;
            default:
                Debug.Log(message: $"{button.name} pressed but has no action");
                break;
        }
    }
}
