using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SubMovement : MonoBehaviour {

    public float Speed = 5;
    public float RotOffset = 90f;

    private bool _rotating = false;
    private Vector3 _lookDir = new Vector3(0, 0, 0);
    private Rigidbody2D _rigidBody = null;

    public void Move(Vector3 dir)
    {
        _rigidBody.velocity = dir * Speed;
    }

    public void LookAt(Vector3 direction)
    {
        _lookDir = direction;
    }

	// Use this for initialization
	void Awake () {
        _rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float angle = MathUtil.Vector2ToAngle(_lookDir);
        transform.rotation = Quaternion.AngleAxis(angle + RotOffset, -Vector3.back);
	}
}
