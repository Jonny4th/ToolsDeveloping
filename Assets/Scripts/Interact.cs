using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    
    public class Interact : MonoBehaviour
    {
        public event Action OnVisitorEnter;
        public event Action OnVisitorLeave;
        private GameObject currentUser = null;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other) {
            if (currentUser == null)
            {
                currentUser = other.gameObject;
                if (currentUser.CompareTag("Visitor")) OnVisitorEnter?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other.gameObject == currentUser) 
            {
                OnVisitorLeave?.Invoke();
                currentUser = null;
            }
        }
    }

}
