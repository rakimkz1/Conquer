using System;
using UnityEngine;

namespace BattleField
{
    public class MagicViewProjectile : MonoBehaviour
    {
        private bool _isShoot;
        public AnimationCurve flyPath;
        public float magicMinHieght;
        public float magicMaxHight;
        public event Action<MagicViewProjectile> OnMagicHit;

        private Vector3 _initialPoint;
        private Vector3 _endPosition;
        private float _flyingTime;
        private float _nowFlyingTime;
        private float _targetOffset;
        private float _hightTarget;
        public void CastMagic(Vector3 intialPos, Vector3 endPosition, float flyingTime, Sprite sprite)
        {
            _initialPoint = intialPos;
            _endPosition = endPosition;
            _flyingTime = flyingTime;
            _isShoot = true;
            _nowFlyingTime = 0f;
            _targetOffset = UnityEngine.Random.Range(0.1f, 3f);
            _hightTarget = UnityEngine.Random.Range(magicMinHieght, magicMaxHight);
            gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

        private void Update()
        {
            if (!_isShoot)
                return;
            if (_nowFlyingTime >= _flyingTime)
            {
                HideMagic();
            }

            float lerp = _nowFlyingTime / _flyingTime;
            transform.position = GetArrowPosition(lerp);
            Vector3 arrowAxis = (GetArrowPosition(Mathf.Clamp(lerp + 0.02f, 0f, 1f)) - GetArrowPosition(lerp)).normalized;
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Cos(arrowAxis.x), Mathf.Sin(arrowAxis.y), 0f));
            _nowFlyingTime += Time.deltaTime;
        }
        private Vector3 GetArrowPosition(float lerp)
        {
            return Vector3.Lerp(_initialPoint, _endPosition + _targetOffset * Vector3.up, lerp) + flyPath.Evaluate(lerp) * _hightTarget * Vector3.up;
        }
        private void HideMagic()
        {
            _isShoot = false;
            gameObject.SetActive(false);
            OnMagicHit?.Invoke(this);
        }
    }
}
