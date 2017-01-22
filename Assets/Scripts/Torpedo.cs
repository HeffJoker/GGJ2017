using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {

    public float Speed = 100f;
    public float TimeToLive = 2f;
    public ParticleSystem ExplosionParticles;
    public string TargetTag;
    public string TargetTag2;
    

    private Rigidbody2D _rigidBody = null;
    private SpriteRenderer _sprite = null;
    private BoxCollider2D _bBox = null;
    private AudioSource _audio = null;

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
        _bBox = GetComponent<BoxCollider2D>();
        _audio = GetComponentInChildren<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag(TargetTag) || (!string.IsNullOrEmpty(TargetTag2) && collider.CompareTag(TargetTag2)))
        {
            Health health = collider.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage();
            }

            Explode();
        }
    }

    private void OnEnable()
    {
        _sprite.enabled = true;
        _bBox.enabled = true;
    }

    private void Explode()
    {
        if (_audio != null)
            _audio.Play();

        _bBox.enabled = false;
        _rigidBody.velocity = Vector2.zero;

        if (ExplosionParticles != null)
            ExplosionParticles.Play();

        StopAllCoroutines();

        StartCoroutine(DisableAfterParticles());
    }

    #region Coroutines

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
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
