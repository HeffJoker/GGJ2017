using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaBoss : MonoBehaviour {

    public float MinDecisionTime = 2;
    public float MaxDecisionTime = 5;

    public ObjectLauncher UnitLauncher;
    public ObjectLauncher[] MineLaunchers;

    public GameObject Player;
    
    private int _numTurrets = 0;
    private int _decisionIndex = 0;

    private EnemyMovement _move;

    public void NotifyOfTurretDeath()
    {
        --_numTurrets;

        if (_numTurrets <= 0)
            Die();
    }

	// Use this for initialization
	void Start () {
        BossTurret[] turrets = GetComponentsInChildren<BossTurret>();

        _numTurrets = turrets.Length;

        foreach(BossTurret turret in turrets)
        {
            turret.SetBoss(this);
        }

        _move = GetComponent<EnemyMovement>();
        _move.DoRotate = false;
        _move.FaceDirection((Player.transform.position - transform.position).normalized);

        StartCoroutine(MakeDecisions());
	}
	
    private void Die()
    {

    }

    private IEnumerator MakeDecisions()
    {
        while(_numTurrets > 0)
        {
            _decisionIndex = Random.Range(0, 3);

            switch(_decisionIndex)
            {
                case 0: Move(); break;
                case 1: LaunchMines(); break;
                case 2: LaunchUnits(); break;
                default: Move(); break;
            }

            float waitTime = Random.Range(MinDecisionTime, MaxDecisionTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Move()
    {
        _move.DoRotate = true;
        _move.Wander();
    }

    private void LaunchMines()
    {
        for (int i = 0; i < MineLaunchers.Length; ++i)
            MineLaunchers[i].Launch();
    }

    private void LaunchUnits()
    {
        _move.Stop();
        _move.DoRotate = false;
        _move.FaceDirection((Player.transform.position - transform.position).normalized);
        GameObject obj = UnitLauncher.Launch();

        Kamikazee km = obj.GetComponent<Kamikazee>();
        km.SetTarget(Player);
    }
}
