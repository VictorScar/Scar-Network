using DG.Tweening;
using UnityEngine;

namespace ScarToolkit.ScarUI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Scale", fileName = "ScaleUI")]
    public class ScaleUI : UIAnimator
    {
        public float startValue = 1;
        public float endValue;


        protected override Tween AnimateInternal(UIView view)
        {
           // Debug.Log("ScaleUI start");
            var animationInternal = DOTween.Sequence();
            animationInternal
                .Append(view.Rect.DOScale(endValue, duration).SetEase(ease));
            return animationInternal;
        }


        protected override void OnStartAnimation(UIView view)
        {
            view.Rect.localScale = new Vector3(startValue, startValue, startValue);
        }

        protected override void OnEndAnimation()
        {
            /*if (_cashedView)
            {
                _cashedView.Rect.localScale = new Vector3(endValue, endValue, endValue);
            }*/
        }

        public override UIAnimator GetInstance()
        {
            var instance = ScriptableObject.CreateInstance<ScaleUI>();
            instance.startValue = startValue;
            instance.endValue = endValue;
            instance.duration = duration;
            instance.ease = ease;

            return instance;
        }
    }
}