using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class GateKeepingEvents : MonoBehaviour
    {
        [SerializeField] List<ScriptableTags> AllowTags = new List<ScriptableTags>();
        public event Action OnUserEnter;
        public event Action OnUserLeave;
        private GameObject currentUser = null;
        private void OnTriggerEnter(Collider other) {
            if (currentUser == null && other.gameObject.GetComponent<TagHolder>().IsOverlap(AllowTags))
            {
                currentUser = other.gameObject;
                OnUserEnter?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other?.gameObject == currentUser)
            {
                // if (currentUser.GetComponent<TagHolder>().IsOverlap(AllowTags))
                // {
                currentUser = null;
                OnUserLeave?.Invoke();
                // }
            }
        }
    }
}
