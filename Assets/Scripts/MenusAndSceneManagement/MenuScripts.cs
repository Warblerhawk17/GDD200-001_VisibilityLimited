using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScripts : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject currentMenu;

    public Button button;
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
                currentMenu.SetActive(false);
                mainMenu.SetActive(true);
                break;
            case 1:
                mainMenu.SetActive(false);
                currentMenu.SetActive(true);
                break;
            case 2:
                //Debug.Log("Resume button pressed");
                currentMenu.SetActive(false);
                break;
            case 3:
                //Debug.Log("Exit button pressed");
                Application.Quit();
                break;
            case 4:
                SceneManager.LoadScene(0);
                break;
            case 5:
                SceneManager.LoadScene(1);
                break;
            default:
                Debug.Log(message: $"{button.name} pressed but has no action");
                break;
        }
    }
}
