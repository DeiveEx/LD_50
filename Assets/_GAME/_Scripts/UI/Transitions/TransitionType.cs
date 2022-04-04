using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME._Scripts.UI.Transitions
{
    public abstract class TransitionType : ScriptableObject
    {
        protected abstract bool Applied(Image image, AnimationCurve curve, float speed, float timePassed);

        protected abstract void UpdateTransition(Image image, AnimationCurve curve, float speed, float timePassed);

        protected virtual void Setup(Image image)
        {
        }

        public UniTask Apply(Image image, AnimationCurve curve, float speed)
        {
            var timePassed = 0f;
            Setup(image);
            return UniTask.WaitUntil(() =>
            {
                timePassed += Time.deltaTime;
                UpdateTransition(image, curve, speed, timePassed);
                return Applied(image, curve, speed, timePassed);
            });
        }
    }
}