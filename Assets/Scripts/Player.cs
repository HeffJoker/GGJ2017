using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private void OnDisable()
    {
        GameStateManager.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("kamikazee"))
        {
            Health health = GetComponent<Health>();
            health.TakeDamage();
        }
    }
}
