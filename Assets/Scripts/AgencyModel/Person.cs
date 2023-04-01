using FacilityRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AgentRelated
{
    public class Person : MonoBehaviour
    {
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
