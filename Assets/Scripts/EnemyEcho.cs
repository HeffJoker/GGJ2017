using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyEcho : MonoBehaviour {


    private Animator _animator;
    private EnemyBehavior _behavior;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _behavior = GetComponentInParent<EnemyBehavior>();
	}
	
	public void DoEcho()
    {
        _animator.SetBool("DoEcho", true);
        StartCoroutine(StopEcho());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
            _behavior.NotifyOfPlayer(collider.transform.position);
    }

    private IEnumerator StopEcho()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("DoEcho", false);
    }
}
