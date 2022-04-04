using UnityEditor;
using UnityEngine;

namespace _GAME._Scripts.UI
{
    public class PowerController : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}