using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject currentMenu;

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
        if (index == 0)
        {
            currentMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (index == 1)
        {
            mainMenu.gameObject.SetActive(false);
            currentMenu.gameObject.SetActive(true);
        }
        else if (index == 2)
        {
            Debug.Log("button pressed");
            Application.Quit();
        }
    }
}
