using UnityEngine;
using UnityEngine.UI;

namespace _GAME._Scripts.UI.Settings
{
    public class FullscreenController : MonoBehaviour
    {
        public Toggle toggle;

        private void Start() => toggle.isOn = Screen.fullScreen;
    }
}