using UnityEngine;
using UnityEngine.UI;

namespace _GAME._Scripts.Sounds
{
    public class MixerGroupVolumeSetter : MonoBehaviour
    {
        public MixerGroupData groupData;
        public Slider slider;

        [SerializeField] private float minValue = -30f;
        [SerializeField] private float maxValue;

        private void Start()
        {
            slider.value = slider.minValue + 
                           (slider.maxValue - slider.minValue) * .5f;
        }

        private static float GetSlider01Value(float value, float min, float max) =>
            (value - min) / (max - min);

        public void SetVolume(float value)
        {
            var sliderValue = minValue + (maxValue - minValue) *
                GetSlider01Value(value, slider.minValue, slider.maxValue);
            var mixerGroup = groupData.mixerGroup;
            mixerGroup.audioMixer.SetFloat(
                $"{mixerGroup.name}_Volume",
                value == 0 ? -80f : sliderValue);
        }
    }
}