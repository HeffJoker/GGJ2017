using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanger : MonoBehaviour
{

    //private Canvas[] _canvases;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnTitleScreen()
    {
        var _canvases = gameObject.GetComponents<Canvas>();
        foreach (Canvas canvas in _canvases)
        {
            if (canvas.name == "Canvas_Title")
                canvas.gameObject.SetActive(true);
            else
                canvas.gameObject.SetActive(false);
            //Do something like door.blah = blah;
        }
    }
    public void TurnOnCreditScreen()
    {
        var _canvases = this.gameObject.GetComponents<Canvas>();
        foreach (Canvas canvas in _canvases)
        {
            if (canvas.name == "Canvas_Credits")
                canvas.gameObject.SetActive(true);
            else
                canvas.gameObject.SetActive(false);
            //Do something like door.blah = blah;
        }
    }
}