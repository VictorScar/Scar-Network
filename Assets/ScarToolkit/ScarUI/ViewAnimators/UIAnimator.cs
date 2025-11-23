using DG.Tweening;
using UnityEngine;

namespace ScarToolkit.ScarUI.ViewAnimators
{
    public abstract class UIAnimator : ScriptableObject
    {
        public float duration;
        public Ease ease = Ease.OutQuad;
        
        private Sequence _animation;
        protected UIView _cashedView;

        public void Init(UIView view)
        {
            _cashedView = view;
            OnInit(view);
        }

        public Tween PlayAnimation(UIView view)
        {
            _cashedView = view;
            OnStartAnimation(view);
           _animation  = DOTween.Sequence();
            _animation.Append(AnimateInternal(view).OnKill(OnEndAnimation));
            return _animation;
        }

        protected abstract Tween AnimateInternal(UIView view);

        public void Kill()
        {
            _animation?.Kill();
        }

        protected abstract void OnStartAnimation(UIView view);
        protected abstract void OnEndAnimation();
        
        protected virtual void OnInit(UIView view)
        {
           
        }

        public abstract UIAnimator GetInstance();

        public void Complete()
        {
            _animation?.Complete();
        }
    }
}
