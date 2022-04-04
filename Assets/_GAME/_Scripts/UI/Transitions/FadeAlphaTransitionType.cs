using System;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME._Scripts.UI.Transitions
{
    [CreateAssetMenu(menuName = "Transitions/FadeAlphaTransitionType",
        fileName = "FadeAlphaTransitionType")]
    public class FadeAlphaTransitionType : TransitionType
    {
        [SerializeField] private float targetAlpha;

        protected override bool Applied(
            Image image,
            AnimationCurve curve,
            float speed,
            float timePassed) =>
            Math.Abs(image.color.a - targetAlpha) < Mathf.Epsilon;

        protected override void UpdateTransition(Image image, AnimationCurve curve, float speed, float timePassed)
        {
            var delta = targetAlpha - _initialAlphaValue;
            var curveValue = curve.Evaluate(timePassed * speed);

            var color = image.color;
            color.a = _initialAlphaValue + delta * curveValue;
            image.color = color;
        }

        private float _initialAlphaValue;

        protected override void Setup(Image image)
        {
            _initialAlphaValue = image.color.a;

            base.Setup(image);
        }
    }
}