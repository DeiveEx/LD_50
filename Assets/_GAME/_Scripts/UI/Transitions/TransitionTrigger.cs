using UnityEngine;
using UnityEngine.Events;

namespace _GAME._Scripts.UI.Transitions
{
    public class TransitionTrigger : MonoBehaviour
    {
        [Header("Transition Settings")]
        [SerializeField] private TransitionType transitionType;
        [SerializeField] private AnimationCurve transitionCurve;
        [SerializeField] private float transitionSpeed;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onTransitionStarted;
        [SerializeField] private UnityEvent onTransitionEnded;

        private TransitionsManager _manager;

        private void Awake() => _manager = TransitionsManager.instance;

        public async void Trigger()
        {
            onTransitionStarted.Invoke();
            
            var result = await _manager.StartTransitionAsync(
                transitionType, 
                transitionCurve, 
                transitionSpeed);
            if (!result) return;
            
            onTransitionEnded.Invoke();
        }
    }
}