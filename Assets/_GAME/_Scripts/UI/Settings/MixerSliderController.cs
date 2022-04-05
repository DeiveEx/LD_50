using System;
using TMPro;
using UnityEngine;

namespace _GAME._Scripts.Sounds
{
    public class MixerSliderController : MonoBehaviour
    {
        public MixerGroupData groupData;
        public TextMeshProUGUI titleText;

        private void Update()
        {
            titleText.text = groupData.mixerGroup.name + ": ";
        }
    }
}