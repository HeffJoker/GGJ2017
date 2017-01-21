using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int Health = 10;

    private int _currHealth = 0;
    private EchoIndicator _indicator;

	// Use this for initialization
	void Awake () {
        tag = "enemy";
        _indicator = GetComponentInChildren<EchoIndicator>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player_torpedo"))
            TakeDamage();
        else if(collider.CompareTag("echo"))
        {
            if (!_indicator.IsShowing)
                _indicator.Show();
        }
    }

    private void OnEnable()
    {
        _currHealth = Health;
    }
	
    private void TakeDamage()
    {
        --_currHealth;

        if (_currHealth <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
