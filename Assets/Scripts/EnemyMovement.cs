using UnityEngine;
using System.Collections;

namespace Treatment.Scripts.Descriptors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        public float Speed = 10f;
        public float MinChangeTime = 2f;
        public float MaxChangeTime = 5f;

        private Vector2 _direction = Vector2.zero;
        private Rigidbody2D _rigidBody;
        private bool _isMoving = false;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            StartCoroutine(ChooseDirection());           
        }

        private void Update()
        {
            _rigidBody.velocity = _direction.normalized * Speed;
            float angle = MathUtil.Vector2ToAngle(_direction);
            transform.rotation = Quaternion.AngleAxis(angle - 90, -Vector3.back);
        }

        private IEnumerator ChooseDirection()
        {
            _isMoving = true;

            while(_isMoving)
            {
                _direction = Random.insideUnitCircle;

                float waitTime = Random.Range(MinChangeTime, MaxChangeTime);

                yield return new WaitForSeconds(waitTime);

                Deccelerate();
            }
        }

        private void Deccelerate()
        {

        }
    }
}