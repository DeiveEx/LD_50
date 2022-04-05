using UnityEngine;

namespace _GAME._Scripts.Sounds
{
    [CreateAssetMenu(fileName = "SoundPlayer",
        menuName = "Sound Player")]
    public class SoundPlayer : ScriptableObject
    {
        [SerializeField] private AudioSource syncSource;
        [SerializeField] private AudioSource[] sources;
        
        public void PlaySync(AudioClip clip)
        {
            
        }
    }
}