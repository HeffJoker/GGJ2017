using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour {

    public ObjectPool ProjectilePool;


    public void Fire(Vector3 direction)
    {
        GameObject obj = ProjectilePool.GetObject(true);
        Torpedo torpedo = obj.GetComponent<Torpedo>();

        torpedo.Fire(direction, transform.position);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
