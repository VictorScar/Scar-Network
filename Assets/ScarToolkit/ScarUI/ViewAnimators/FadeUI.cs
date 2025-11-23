using DG.Tweening;
using UnityEngine;

namespace ScarToolkit.ScarUI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Fade", fileName = "FadeUI")]
    public class FadeUI : UIAnimator
    {
        [SerializeField] protected float startValue = 1;
        [SerializeField] protected float endValue = 1;

        protected override Tween AnimateInternal(UIView view)
        {
            var internalAnimation = DOTween.Sequence();
            internalAnimation
                .Append(view.CG.DOFade(endValue, duration).SetEase(ease));
            return internalAnimation;
        }

        protected override void OnStartAnimation(UIView view)
        {
            view.CG.alpha = startValue;
        }

        protected override void OnEndAnimation()
        {
           if(_cashedView) _cashedView.CG.alpha = endValue;
        }

        public override UIAnimator GetInstance()
        {
            var instance = CreateInstance<FadeUI>();
            instance.startValue = startValue;
            instance.endValue = endValue;
            instance.duration = duration;
            instance.ease = ease;

            return instance;
        }
    }
}