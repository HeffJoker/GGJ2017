using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float MaxFireDist = 5f;
    public float MinAttackTime = 2f;
    public float MaxAttackTime = 5f;

    public float WaitTime = 2f;
    public float DebounceTime = 1f;

    public WeaponSlot[] Weapons;
    public EnemyMovement Move;

    private EnemyEcho _echo;


    private bool _foundPlayer = false;
    private Coroutine _waitRoutine;

	// Use this for initialization
	void Start () {
        //_move = GetComponent<EnemyMovement>();
        _echo = GetComponentInChildren<EnemyEcho>();
        //_weapons = GetComponentsInChildren<WeaponSlot>();

        _waitRoutine = StartCoroutine(DoWait());
	}

    public void NotifyOfPlayer(Vector3 position)
    {
        StopCoroutine(_waitRoutine);
        _foundPlayer = true;

        float dist = Vector3.Distance(transform.position, position);

        if (dist > MaxFireDist)
            StartCoroutine(MoveTo(transform.position + (position - transform.position).normalized * dist / 2, position));
        else
            StartCoroutine(AttackPosition(position));
    }

    private IEnumerator MoveTo(Vector3  movePos, Vector3 target)
    {
        Move.DoRotate = true;
        Move.MoveTo(movePos);
        while(!Move.AtTarget)
        {
            yield return null;
        }

        yield return AttackPosition(target);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator AttackPosition(Vector3 target)
    {
        int currWeapon = 0;
        float attackTime = Random.Range(MinAttackTime, MaxAttackTime);
        float currTime = 0;
        float currDebounceTime = 0;

        Move.FaceDirection((target - transform.position).normalized);
        Move.DoRotate = false;
        Move.Stop();

        while(currTime <= attackTime)
        {
            if(currDebounceTime <= 0)
            {
                WeaponSlot weapon = Weapons[currWeapon];
                weapon.Fire((target - transform.position).normalized);

                ++currWeapon;

                if (currWeapon >= Weapons.Length)
                    currWeapon = 0;

                currDebounceTime = DebounceTime;
            }

            currDebounceTime -= Time.deltaTime;
            currTime += Time.deltaTime;

            yield return null;
        }

        _waitRoutine = StartCoroutine(DoWait());
    }

    private void DoEcho()
    {
        _foundPlayer = false;
        _echo.DoEcho();

        _waitRoutine = StartCoroutine(DoWait());
    }

    private IEnumerator DoWait()
    {
        Move.Stop();
        Move.Wander();

        yield return new WaitForSeconds(WaitTime);

        DoEcho();
    }
}
