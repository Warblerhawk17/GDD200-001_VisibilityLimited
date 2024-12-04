using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    // Start is called before the first frame update
    public static DamageFlash instance;
    public Image image;
    public float defaultAlpha;
    public float interval;
    public float decayAmmount;

    void Start()
    {
        
    }

    private void Awake() //On Awake sets instance to this
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator flash()
    {
        //Debug.Log("Flash called");
        image.color = new Color(image.color.r, image.color.g, image.color.b, defaultAlpha);
        while (image.color.a > 0)
        {
            yield return new WaitForSeconds(interval);
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - decayAmmount);
        }
    }
}
