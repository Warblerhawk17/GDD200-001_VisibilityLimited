using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipToPlay : MonoBehaviour
{
    public Image button;
    public Sprite newButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeButton());
    }

    // Update is called once per frame
    private IEnumerator ChangeButton()
    {
        yield return new WaitForSeconds(35);
        button.sprite = newButton;
    }
}
