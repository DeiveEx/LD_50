using UnityEngine;

namespace _GAME._Scripts.ScreenResolution
{
    public class FullscreenSetter : MonoBehaviour
    {
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}