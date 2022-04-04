using UnityEngine;

namespace _GAME._Scripts.UI.Settings
{
    public class MenuTransition : MonoBehaviour
    {
        public RectTransform container;
        public float speed = 4f;

        public RectTransform target;
        public RectTransform openedReference;
        public RectTransform closedReference;

        public void Open() => SetTarget(openedReference);
        public void Close() => SetTarget(closedReference);

        public void SetTarget(RectTransform rectTransform) => 
            target = rectTransform;

        private void Update()
        {
            container.position = Vector3.Lerp(
                container.position, 
                target.position, 
                Time.deltaTime * speed);
        }
    }
}