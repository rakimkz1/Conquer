
using System;
using UnityEngine;

namespace BattleField
{
    public class ArrowViewProjectile : MonoBehaviour
    {
        private Vector3 _initialPoint;
        private Transform _target;
        private float _flyingTime;
        private float _nowFlyingTime;
        private bool _isShoot;
        public AnimationCurve flyPath;
        public float arrowHieght;
        public event Action<ArrowViewProjectile> OnArrowHit;

        public void ShootArrow(Vector3 initialPoint, Transform target, float flyingTime, Sprite sprite)
        {
            gameObject.SetActive(true);
            _initialPoint = initialPoint;
            _target = target;
            _flyingTime = flyingTime;
            _nowFlyingTime = 0f;
            _isShoot = true;
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
            transform.position = GetArrowPosition(lerp);
            Vector3 arrowAxis = (GetArrowPosition(Mathf.Clamp(lerp + 0.02f, 0f, 1f)) - GetArrowPosition(lerp)).normalized;
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Cos(arrowAxis.x), Mathf.Sin(arrowAxis.y), 0f));
            _nowFlyingTime += Time.deltaTime;
        }

        private Vector3 GetArrowPosition(float lerp)
        {
            return Vector3.Lerp(_initialPoint, _target.position, lerp) + flyPath.Evaluate(lerp) * arrowHieght * Vector3.up;
        }

        private void HideArrow()
        {
            OnArrowHit?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
