using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME._Scripts.UI.Transitions
{
    public class TransitionsManager : Singleton<TransitionsManager>
    {
        [SerializeField] private Image transitionImage;
        [SerializeField] private AnimationCurve defaultCurve;
        [SerializeField] private float defaultSpeed;

        private void OnEnable() =>
            transitionImage.raycastTarget = false;

        private bool _isTransitioning;

        public async UniTask<bool> StartTransitionAsync(TransitionType type, AnimationCurve curve, float speed)
        {
            transitionImage.raycastTarget = true;
            if (speed == 0f) speed = defaultSpeed;
            if (curve.length == 0) curve = defaultCurve;

            if (_isTransitioning) return false;

            _isTransitioning = true;
            await type.Apply(transitionImage, curve, speed);
            _isTransitioning = false;

            transitionImage.raycastTarget = false;

            return true;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            transitionImage.enabled = EditorApplication.isPlaying;
        }
#endif
    }
}