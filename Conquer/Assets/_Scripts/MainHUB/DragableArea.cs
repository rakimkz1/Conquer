using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.MainHUB
{
    public class DragableArea : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public event Action OnDrag;
        [SerializeField] private float 
        public void OnBeginDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}