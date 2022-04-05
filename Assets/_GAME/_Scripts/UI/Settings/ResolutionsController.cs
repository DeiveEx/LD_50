using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _GAME._Scripts.UI.Settings
{
    public class ResolutionsController : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        private void Start()
        {
            var availableResolutions = Screen.resolutions;
            
            dropdown.options.Clear();
            for (var index = 0; index < availableResolutions.Length; index++)
            {
                var resolution = availableResolutions[index];
                var nextResolution = availableResolutions.Length == index + 1
                    ? default
                    : availableResolutions[index + 1];
                if (nextResolution.width == resolution.width &&
                    nextResolution.height == resolution.height)
                    continue;
                
                var resolutionString = resolution.width + " X " + resolution.height;
                dropdown.options.Add(new TMP_Dropdown.OptionData
                {
                    text = resolutionString
                });
                
                if (Screen.currentResolution.Equals(resolution)) dropdown.value = dropdown.options.Count - 1;
            }
        }

        public void OnSelectOption(int index)
        {
            var resolution = dropdown.options[index].text
                .Split('X')
                .Select(t => Convert.ToInt32(t))
                .ToArray();
            Screen.SetResolution(resolution[0], resolution[1], Screen.fullScreen);
        }
    }
}