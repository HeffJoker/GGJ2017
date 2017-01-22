using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour {

    public Vector3 Direction = Vector3.zero;
    public ObjectPool ObjectPool;
    public float LaunchSpeed;

    public void Launch()
    {
        GameObject obj = ObjectPool.GetObject(true);
        obj.transform.position = transform.position;

        Rigidbody2D body = obj.GetComponent<Rigidbody2D>();
        Vector3 dir = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.z, -Vector3.back) * Direction;

        body.AddForce(dir * LaunchSpeed);
    }
	
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 dir = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.z, -Vector3.back) * Direction;
        Gizmos.DrawLine(transform.position, transform.position + dir);
    }
}
