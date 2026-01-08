using System;
using UnityEngine;

namespace BattleField
{
    public class SiegeViewProjectile : MonoBehaviour
    {
        private Vector3 _initialPoint;
        private Transform _target;
        private float _flyingTime;
        private float _nowFlyingTime;
        private bool _isShoot;
        public AnimationCurve flyPath;
        public float siegeHieght;
        public event Action<SiegeViewProjectile> OnSiegeHit;

        public void ShootSiege(Vector3 initialPoint, Transform target, float flyingTime, Sprite sprite)
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

            if (_nowFlyingTime >= _flyingTime)
            {
                _isShoot = false;
                HideSiege();
            }

            float lerp = _nowFlyingTime / _flyingTime;
            transform.position = GetSiegePosition(lerp);
            Vector3 arrowAxis = (GetSiegePosition(Mathf.Clamp(lerp + 0.02f, 0f, 1f)) - GetSiegePosition(lerp)).normalized;
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Cos(arrowAxis.x), Mathf.Sin(arrowAxis.y), 0f));
            _nowFlyingTime += Time.deltaTime;
        }

        private Vector3 GetSiegePosition(float lerp)
        {
            return Vector3.Lerp(_initialPoint, _target.position, lerp) + flyPath.Evaluate(lerp) * siegeHieght * Vector3.up;
        }

        private void HideSiege()
        {
            OnSiegeHit?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
