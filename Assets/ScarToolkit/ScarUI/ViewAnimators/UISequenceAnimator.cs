using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ScarToolkit.ScarUI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Sequence", fileName = "SequenceUI")]
    public class UISequenceAnimator : UIAnimator
    {
        public UIAnimator[] animators;

        protected override void OnInit(UIView view)
        {
            if (animators != null)
            {
                foreach (var animator in animators)
                {
                    animator.Init(view);
                }
            }
        }

        public override UIAnimator GetInstance()
        {
            var instance = CreateInstance<UISequenceAnimator>();

            var animatorsBuffer = new List<UIAnimator>();

            if (animators != null)
            {
                foreach (var animator in animators)
                {
                    animatorsBuffer.Add(animator.GetInstance());
                }

                instance.animators = animatorsBuffer.ToArray();
            }

            return instance;
        }


        protected override Tween AnimateInternal(UIView view)
        {
            if (animators != null)
            {
                var animationInternal = DOTween.Sequence();

                foreach (var animator in animators)
                {
                    animationInternal.Append(animator.PlayAnimation(view));
                }

                animationInternal.Play();

                return animationInternal;
            }

            return null;
        }

        protected override void OnStartAnimation(UIView view)
        {
        }

        protected override void OnEndAnimation()
        {
        }
    }
}