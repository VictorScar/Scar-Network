using DG.Tweening;
using ScarToolkit.Button;
using ScarToolkit.ScarUI.ViewAnimators;
using UnityEngine;

namespace ScarToolkit.ScarUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIView : MonoBehaviour
    {
        [Header("Default References")] [SerializeField]
        protected RectTransform rect;
        [SerializeField] private bool showOnInit = false;

        [SerializeField] private CanvasGroup cg;

        [Header("Animations")] [SerializeField]
        private UIAnimator showInAnimator;

        [SerializeField] private UIAnimator hideInAnimator;


        public RectTransform Rect => rect;
        public CanvasGroup CG => cg;

        public void Init()
        {
            if (!cg)
            {
                cg = GetComponent<CanvasGroup>();
            }

            if (showInAnimator)
            {
                showInAnimator = showInAnimator.GetInstance();
                showInAnimator?.Init(this);
            }

            if (hideInAnimator)
            {
                hideInAnimator = hideInAnimator.GetInstance();
                hideInAnimator?.Init(this);
            }

            if (showOnInit)
            {
                Show(true);
            }
            else
            {
                Hide(true);
            }

            OnInit();
        }

        [Button("Show")]
        public void DebugShow()
        {
            Show();
        }

        [Button("Hide")]
        public void DebugHide()
        {
            Hide();
        }

        [Button("Init")]
        public void DebugInit()
        {
            Init();
        }

        [Button("Kill")]
        public void DebugKill()
        {
            showInAnimator?.Kill();
            hideInAnimator?.Kill();
        }

        public void Show(bool immediately = false)
        {
            showInAnimator?.Kill();
            hideInAnimator?.Kill();
            
            if (!immediately)
            {
                if (showInAnimator)
                {
                    showInAnimator?.PlayAnimation(this).OnKill(ShowInternal);
                    gameObject.SetActive(true);
                }
                else
                {
                    ShowInternal();
                }
            }
            else
            {
                ShowInternal();
            }
        }

        public void Hide(bool immediately = false)
        {
            showInAnimator?.Kill();
           // hideInAnimator?.Complete();

            if (!immediately)
            {
                if (hideInAnimator)
                {
                    hideInAnimator?.PlayAnimation(this).OnKill(HideInternal);
                }
                else
                {
                    HideInternal();
                }
            }
            else
            {
                gameObject.SetActive(false);
                OnHide();
            }
        }

        private void ShowInternal()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        private void HideInternal()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnShow()
        {
            // Debug.Log("OnShow");
        }

        protected virtual void OnHide()
        {
            // Debug.Log("OnHide");
        }
    }
}