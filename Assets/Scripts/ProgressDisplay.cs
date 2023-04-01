using UnityEngine;
using UnityEngine.UI;
using FacilityRelated;

namespace ToolTesting
{
    public class ProgressDisplay : MonoBehaviour
    {
        [SerializeField] private FacilityStatValue facility;
        [SerializeField] private Image progressDisplayImage;

        private void Update()
        {
            UpdateProgressbar();
        }
        public void UpdateProgressbar()
        {
            progressDisplayImage.fillAmount = Mathf.Clamp((float)facility.CurrentValue/(float)facility.ValueMax,0,1f);
        }

    }

}
