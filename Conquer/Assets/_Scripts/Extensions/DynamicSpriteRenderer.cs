using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class CustomDynamicSpriteRenderer : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    private SpriteRenderer _spriteRenderer;
    private int _exptantion = 10000;
    private int _initialRenderOrder;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialRenderOrder = _spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        _spriteRenderer.sortingOrder = _initialRenderOrder - (int)(rootTransform.position.y * _exptantion);
    }
}
