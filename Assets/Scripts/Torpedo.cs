using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {

    public float Speed = 100f;
    public float TimeToLive = 2f;

    private Rigidbody2D _rigidBody = null;

    public void Fire(Vector3 direction, Vector3 position)
    {
        _rigidBody.AddForce(direction * Speed);
        StartCoroutine(DeactivateAfterTime());
        transform.position = position;

        float angle = MathUtil.Vector2ToAngle(direction);
        transform.rotation = Quaternion.AngleAxis(angle-90, -Vector3.back);
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(TimeToLive);
        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Awake () {
        _rigidBody = GetComponent<Rigidbody2D>();
	}
}
