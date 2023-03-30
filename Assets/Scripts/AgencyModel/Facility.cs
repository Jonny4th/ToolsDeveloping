using Agent;
using ToolTesting;
using UnityEngine;

namespace Facility
{
    public class Facility : MonoBehaviour, IInteractable
    {
        [SerializeField] FacilityStatValue statValue;
        public virtual void Interact(Person visitor) 
        {
            if (statValue.currentValue > 0)
            {
                visitor.GetComponent<VisitorStatValue>().ModifyValue(5f);
                statValue.DeductValue();
            }
        }
    }
}
