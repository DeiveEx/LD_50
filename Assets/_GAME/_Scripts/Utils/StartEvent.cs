using UnityEngine;
using UnityEngine.Events;

namespace _GAME._Scripts.Utils
{
    public class StartEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart;
        
        private void Start() => onStart.Invoke();
    }
}