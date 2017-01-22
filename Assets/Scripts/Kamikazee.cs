using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikazee : MonoBehaviour {

    public float WaitToMove = 0.5f;
    public float TimeToLive = 3f;
    public float MoveSpeed = 3f;

    public GameObject _target;

    private SpriteRenderer _sprite;
    
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void OnEnable()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _sprite.enabled = true;
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(WaitToMove);

        float currTime = 0;
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        body.velocity = Vector2.zero;

        while(currTime <= TimeToLive)
        {
            body.velocity = (_target.transform.position - transform.position).normalized * MoveSpeed;
            float angle = MathUtil.Vector2ToAngle(body.velocity.normalized) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, -Vector3.back);
            
            currTime += Time.deltaTime;
            yield return null;
        }

        Explode(body);
    }

    private void Explode(Rigidbody2D body)
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        body.velocity = Vector2.zero;

        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();

        if (particles != null)
            particles.Play();

        StopAllCoroutines();

        StartCoroutine(DisableAfterParticles(particles));
    }

    private IEnumerator DisableAfterParticles(ParticleSystem particles)
    {
        _sprite.enabled = false;
        while (particles.isPlaying)
            yield return null;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
        {
            StopAllCoroutines();
            Explode(GetComponent<Rigidbody2D>());
        }
    }
}
