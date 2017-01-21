using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int Health = 10;

    private int _currHealth = 0;

	// Use this for initialization
	void Awake () {
        tag = "enemy";

	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player_torpedo"))
            TakeDamage();
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
