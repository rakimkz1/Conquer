
using System;
using UnityEngine;

namespace BattleField
{
    public class ArrowViewProjectile : MonoBehaviour
    {
        public AnimationCurve flyPath;
        public float arrowMinHieght;
        public float arrowMaxHight;
        public event Action<ArrowViewProjectile> OnArrowHit;

        private Vector3 _initialPoint;
        private Transform _target;
        private float _flyingTime;
        private float _nowFlyingTime;
        private bool _isShoot;
        private float _targetOffset;
        private float _hightOffset;
        public void ShootArrow(Vector3 initialPoint, Transform target, float flyingTime, Sprite sprite)
        {
            gameObject.SetActive(true);
            _initialPoint = initialPoint;
            _target = target;
            _flyingTime = flyingTime;
            _nowFlyingTime = 0f;
            _isShoot = true;
            _targetOffset = UnityEngine.Random.Range(0.1f, 3f);
            _hightOffset = UnityEngine.Random.Range(arrowMinHieght, arrowMaxHight);
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

        public void Update()
        {
            if (!_isShoot)
                return;

            if(_nowFlyingTime >= _flyingTime)
            {
                _isShoot = false;
                HideArrow();
            }

            float lerp = _nowFlyingTime / _flyingTime;
            if (_flyingTime == 0f)
                Debug.Log("fly is 0");
            transform.position = GetArrowPosition(lerp);
            Vector3 arrowAxis = (GetArrowPosition(Mathf.Clamp(lerp + 0.1f, 0f, 1f)) - GetArrowPosition(lerp)).normalized;
            Debug.DrawRay(transform.position, arrowAxis, Color.red);
            float angle = Mathf.Atan2(arrowAxis.y, arrowAxis.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _nowFlyingTime += Time.deltaTime;
        }

        private Vector3 GetArrowPosition(float lerp)
        {
            return Vector3.Lerp(_initialPoint, _target.position + Vector3.up * _targetOffset, lerp) + flyPath.Evaluate(lerp) * _hightOffset * Vector3.up;
        }

        private void HideArrow()
        {
            OnArrowHit?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
