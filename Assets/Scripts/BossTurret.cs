using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BossTurret : MonoBehaviour {

    public GameObject Player;
    public float DebounceTime = 1.5f;

    private bool _attackPlayer = false;
    private int _currWeapon = 0;
    private float _currTime = 0;
    private WeaponSlot[] _weapons;
    private MantaBoss _boss;
    
    public void SetBoss(MantaBoss boss)
    {
        _boss = boss;
    }

    private void OnDisable()
    {
        if (_boss != null)
            _boss.NotifyOfTurretDeath();
    }

	// Use this for initialization
	void Start () {
        _weapons = GetComponentsInChildren<WeaponSlot>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(_attackPlayer)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;

            RotateToFace(direction);
            
            // Shoot weapons
            if(_currTime <= 0)
            {
                WeaponSlot weapon = _weapons[_currWeapon];
                weapon.Fire(direction);

                ++_currWeapon;

                if (_currWeapon >= _weapons.Length)
                    _currWeapon = 0;

                _currTime = DebounceTime;
            }

            _currTime -= Time.deltaTime;
        }	
	}

    private void RotateToFace(Vector3 direction)
    {
        float angle = MathUtil.Vector2ToAngle(direction);
        transform.rotation = Quaternion.AngleAxis(angle + 90, -Vector3.back);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
        {
            _attackPlayer = true;
            _currTime = 0;
            _currWeapon = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
            _attackPlayer = false;
    }
}
