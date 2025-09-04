using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float BorderHieght;
    public float BorderWieght;
    public float DragSpeed;
    public float ElasticSpeed;
    public bool IsCameraMovable;
    private Vector3 _targetPosition;
    private Vector3 _velocity;
    private bool _isMoving;
    private void Start()
    {
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (IsAllowedToMove())
        {
            _targetPosition += new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0f) * DragSpeed;
        }

        float clampX = Mathf.Clamp(_targetPosition.x, -BorderWieght, BorderWieght);
        float clampY = Mathf.Clamp(_targetPosition.y, -BorderHieght, BorderHieght);
        _targetPosition = Vector3.Lerp(_targetPosition, new Vector3(clampX, clampY, _targetPosition.z), ElasticSpeed);

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, 0.1f);
    }

    private bool IsAllowedToMove()
    {
        if (!IsCameraMovable)
            return false;
        bool isMouseDown = Input.GetMouseButton(0);
        bool isMouseOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        if(_isMoving && !isMouseDown)
            _isMoving = false;
        if (isMouseDown && !_isMoving && !isMouseOverUI)
        {
            _isMoving = true;
            return true;
        }
        else if (isMouseDown && _isMoving)
            return true;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.up * BorderHieght + Vector3.right * BorderWieght, Vector3.down * BorderHieght + Vector3.right * BorderWieght);
        Gizmos.DrawLine(Vector3.down * BorderHieght + Vector3.right * BorderWieght, Vector3.down * BorderHieght + Vector3.left * BorderWieght);
        Gizmos.DrawLine(Vector3.down * BorderHieght + Vector3.left * BorderWieght, Vector3.up * BorderHieght + Vector3.left * BorderWieght);
        Gizmos.DrawLine(Vector3.up * BorderHieght + Vector3.left * BorderWieght, Vector3.up * BorderHieght + Vector3.right * BorderWieght);
    }
}
