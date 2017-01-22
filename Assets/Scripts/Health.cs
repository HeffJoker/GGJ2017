using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int MaxHealth = 10;
    public float SliderHideTime = 1f;
    public SpriteRenderer Sprite;
    public ParticleSystem ExplodeParticles;
    public GameObject ObjectToDisable;
    public AudioSource ExplodeSound;

    private int _currHealth = 0;
    private EchoIndicator _indicator;
    private Slider _slider;
    private bool _isDead = false;
   
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
        _isDead = false;
    }
	
    public void TakeDamage()
    {
        if (_isDead)
            return;

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
        _isDead = true;

        if (gameObject.tag == "enemy")
            EnemyCountManager.Instance.Decrement();

        if (ExplodeSound != null)
            ExplodeSound.Play();

        if (ExplodeParticles != null)
        {
            ExplodeParticles.Play();
            StartCoroutine(DisableAfterParticles(ExplodeParticles));
        }
        else
        {
            gameObject.SetActive(false);
            if (ObjectToDisable != null)
                ObjectToDisable.SetActive(false);
        }
    }

    private IEnumerator DisableAfterParticles(ParticleSystem particles)
    {
        Sprite.enabled = false;

        yield return new WaitForSeconds(0.5f);
        //while (particles.isPlaying)
        //    yield return null;

        gameObject.SetActive(false);

        if (ObjectToDisable != null)
            ObjectToDisable.SetActive(false);
    }
}
