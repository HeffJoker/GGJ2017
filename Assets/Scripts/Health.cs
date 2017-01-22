﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int MaxHealth = 10;
    public float SliderHideTime = 1f;

    private int _currHealth = 0;
    private EchoIndicator _indicator;
    private Slider _slider;

	// Use this for initialization
	void Awake () {
        _indicator = GetComponentInChildren<EchoIndicator>();
        _slider = GetComponentInChildren<Slider>();

        if(_slider != null)
        {
            _slider.minValue = 0;
            _slider.maxValue = MaxHealth;
            _slider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collider.CompareTag("player_torpedo"))
        //    TakeDamage();
        if(collider.CompareTag("echo"))
        {
            if (!_indicator.IsShowing)
                _indicator.Show();
        }
    }

    private void OnEnable()
    {
        _currHealth = MaxHealth;
    }
	
    public void TakeDamage()
    {       
        --_currHealth;

        if (_slider != null)
        {
            _slider.gameObject.SetActive(true);
            _slider.value = _currHealth;
            StartCoroutine(HideSlider());
        }

        if (_currHealth <= 0)
            Die();
    }

    IEnumerator HideSlider()
    {
        yield return new WaitForSeconds(SliderHideTime);
        _slider.gameObject.SetActive(false);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}