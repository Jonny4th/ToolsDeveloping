using UnityEngine;
using UnityEngine.UI;

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
            progressDisplayImage.fillAmount = Mathf.Clamp((float)facility.value/(float)facility.valueMax,0,1f);
        }

    }

}
