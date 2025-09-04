using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraUnitDraging : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float acceptableTime;
    private bool isDragging = false;
    private CancellationTokenSource token;

    private void Update()
    {
        CheckDragging();
    }
    private void CheckDragging()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        bool isUICrossed = EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && hit.collider == null && !isUICrossed)
        {
            WaitAcceptableTime();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            token?.Cancel();
        }
    }

    private async UniTask WaitAcceptableTime()
    {
        token = new CancellationTokenSource();
        try
        {
            await UniTask.WaitForSeconds(acceptableTime,cancellationToken: token.Token);
        }
        catch
        {
            return;
        }
        isDragging = true;
    }
}
