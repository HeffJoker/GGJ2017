using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float MinChangeTime = 2f;
    public float MaxChangeTime = 5f;
    public float RotOffset = -90;
    public SpriteRenderer Sprite;
    
    public float MaxMoveRadius = 10f;

    private Vector2 _direction = Vector2.zero;
    private Vector3? _target = null;
    private Rigidbody2D _rigidBody;
    private bool _isMoving = false;
    private Coroutine _wanderRoutine;

    public bool AtTarget
    {
        get; private set;
    }

    public bool DoRotate
    {
        get; set;
    }

    public void MoveTo(Vector3 position)
    {
        AtTarget = false;
        StopCoroutine(_wanderRoutine);
        _target = position;
        _direction = (position - transform.position);
    }

    public void Wander()
    {
        _wanderRoutine = StartCoroutine(ChooseDirection());
        DoRotate = true;
    }

    public void FaceDirection(Vector2 direction)
    {
        float angle = MathUtil.Vector2ToAngle(direction);
        Sprite.transform.rotation = Quaternion.AngleAxis(angle + RotOffset, -Vector3.back);
    }

    public void Stop()
    {
        _direction = Vector2.zero;
        _isMoving = false;

        if (_rigidBody != null)
            _rigidBody.velocity = Vector2.zero;
        
        if(_wanderRoutine != null)
            StopCoroutine(_wanderRoutine);
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _wanderRoutine = StartCoroutine(ChooseDirection());

        DoRotate = true;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.Value) <= 0.01f)
            {
                AtTarget = true;
                _target = null;
                _direction = Vector3.zero;
            }
        }

        _rigidBody.velocity = _direction.normalized * Speed;

        if (DoRotate)
        {
            FaceDirection(_direction);
        }
    }

    private IEnumerator ChooseDirection()
    {
        _isMoving = true;

        while (_isMoving)
        {
            float waitTime = Random.Range(MinChangeTime, MaxChangeTime);

            if (OutsideMaxRange())
            {
                _direction = Vector3.zero - transform.position;
                waitTime *= 2f;
            }
            else
                _direction = Random.insideUnitCircle;
                        
            yield return new WaitForSeconds(waitTime);

            Deccelerate();
        }
    }

    private void Deccelerate()
    {

    }

    private bool OutsideMaxRange()
    {
        return (Vector3.Distance(Vector3.zero, transform.position) >= MaxMoveRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(Vector3.zero, MaxMoveRadius);
    }
}