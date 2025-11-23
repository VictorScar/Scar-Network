using DG.Tweening;
using UnityEngine;

namespace ScarToolkit.ScarUI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Rotate", fileName = "RotateUI")]
    public class RotateUI : UIAnimator
    {
        [SerializeField] private Vector3 startValue = Vector3.zero;
        [SerializeField] private Vector3 endValue = new Vector3(0f,0f,360f);
        [SerializeField] private int loopsCount = -1;
        protected override Tween AnimateInternal(UIView view)
        {
            var animationInternal = DOTween.Sequence();
            animationInternal
               
                .Append(view.Rect.DORotate(view.Rect.rotation.eulerAngles + endValue, duration,
                    RotateMode.FastBeyond360))
                .SetEase(ease)
                .SetDelay(0f)
                .SetLoops(loopsCount, LoopType.Restart);
        
            return animationInternal;
        }

        protected override void OnStartAnimation(UIView view)
        {
            view.Rect.rotation = Quaternion.Euler(startValue);
        }

        protected override void OnEndAnimation()
        {
            if(_cashedView) _cashedView.Rect.rotation = Quaternion.Euler(startValue);
        }

        public override UIAnimator GetInstance()
        {
            var instance = CreateInstance<RotateUI>();
            instance.startValue = startValue;
            instance.endValue = endValue;
            instance.duration = duration;
            instance.ease = ease;

            return instance;
        }
    }
}