using Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agent
{
    public class Person : MonoBehaviour
    {
        [SerializeField] IInteractable interactable;
        public void Utilize()
        {
            interactable.Interact(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out interactable);
        }
    }
}
