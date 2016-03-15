using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour
{
    CubeRotator cr;
    public Image imageRef;
   
    void Start()
    {

        cr = GameObject.Find("Cube").GetComponent<CubeRotator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (cr.isVisible)
            BeginText();

    }

    public void BeginText()
    {
        imageRef.CrossFadeColor(Color.clear, 5, false, true);
    }
}
