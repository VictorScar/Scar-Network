using System;
using ScarToolkit.ScarUI.ViewAnimators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScarToolkit.ScarUI
{
    public class UIClickableView : UIView, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private UIAnimator onClickDownAnimator;
        [SerializeField] private UIAnimator onClickUpAnimator;
        public event Action<UIClickableView, PointerEventData> onBtnClick;
        public event Action onClick;

        
        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown(eventData);
        }
        
        protected override void OnInit()
        {
            if (onClickDownAnimator)
            {
                onClickDownAnimator = onClickDownAnimator.GetInstance();
                onClickDownAnimator.Init(this);
            }

            if (onClickUpAnimator)
            {
                onClickUpAnimator = onClickUpAnimator.GetInstance();
                onClickUpAnimator.Init(this);
            }
        }

        public virtual void PointerClick(PointerEventData eventData)
        {
            onBtnClick?.Invoke(this, eventData);
            onClick?.Invoke();
           // Debug.Log("Click!");
           // OnPointerClick(eventData);
        }

        public virtual void PointerUp(PointerEventData eventData)
        {
           onClickUpAnimator?.PlayAnimation(this);
           // Debug.Log("PointerUp");
        }

        public virtual void PointerDown(PointerEventData eventData)
        {
            onClickDownAnimator?.PlayAnimation(this);
           // Debug.Log("PointerDown");
        }

    }
}