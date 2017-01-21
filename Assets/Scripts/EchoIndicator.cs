using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoIndicator : MonoBehaviour {

    public float ShowTime = 2f;
    public Color Color = Color.red;

    private SpriteRenderer _sprite;
    private bool _showing = false;

    public bool IsShowing
    {
        get { return _showing; }
    }

    public void Show()
    {
        StartCoroutine(DisplayIndicator());
    }

    // Use this for initialization
    void Start () {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;
	}	


    private IEnumerator DisplayIndicator()
    {
        _showing = true;
        float currTime = 0;
        float endTime = ShowTime / 2;
        _sprite.enabled = true;
        
        while(currTime <= endTime)
        {
            _sprite.color = Color.Lerp(Color.clear, Color, 2 * currTime / endTime);
            currTime += Time.deltaTime;
            yield return null;
        }

        while(currTime >= 0)
        {
            _sprite.color = Color.Lerp(Color, Color.clear, 1 - (currTime / endTime));
            currTime -= Time.deltaTime;
            yield return null;
        }

        _sprite.enabled = false;
        _showing = false;
    }
}
