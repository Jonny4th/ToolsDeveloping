using FacilityRelated;
using ToolTesting;
using UnityEngine;

namespace AgentRelated
{
    public class Visitor : Person
    {
        [SerializeField] VisitorStatValue statValue;

        [SerializeField] protected IInteractable interactable;

        public virtual void Utilize()
        {
            if(interactable == null) return;
            interactable.Interact(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            interactable = other.GetComponentInParent<IInteractable>();
            //other.TryGetComponent(out interactable);
        }
    }
}