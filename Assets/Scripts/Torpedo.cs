using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {

    public float Speed = 100f;
    public float TimeToLive = 2f;
    public ParticleSystem ExplosionParticles;

    private Rigidbody2D _rigidBody = null;
    private SpriteRenderer _sprite = null;

    public void Fire(Vector3 direction, Vector3 position)
    {
        _rigidBody.AddForce(direction * Speed);
        StartCoroutine(DeactivateAfterTime(TimeToLive));
        transform.position = position;

        float angle = MathUtil.Vector2ToAngle(direction);
        transform.rotation = Quaternion.AngleAxis(angle-90, -Vector3.back);
    }

	// Use this for initialization
	void Awake () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("enemy"))
        {
            _rigidBody.velocity = Vector2.zero;

            if (ExplosionParticles != null)
                ExplosionParticles.Play();

            StopAllCoroutines();
            
            StartCoroutine(DisableAfterParticles());
        }
    }

    private void OnEnable()
    {
        _sprite.enabled = true;
    }

    #region Coroutines

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private IEnumerator DisableAfterParticles()
    {
        _sprite.enabled = false;
        while (ExplosionParticles.isPlaying)
            yield return null;

        gameObject.SetActive(false);
    }

    #endregion Coroutines
}
