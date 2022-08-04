using UnityEngine;
using UnityEngine.UI;

namespace ToolTesting
{
    public class ProgressDisplay : MonoBehaviour
    {
        [SerializeField] private Image progressDisplayImage;
        [SerializeField] private FacilityCleanliness facility;

        private void Update()
        {
            UpdateProgressbar();
        }
        public void UpdateProgressbar()
        {
            progressDisplayImage.fillAmount = Mathf.Clamp((float)facility.cleanliness/(float)facility.cleanlinessMax,0,1f);
        }

    }

}
