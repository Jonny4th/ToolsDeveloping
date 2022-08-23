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
        private TagHolder currentUserTags;
        private void OnTriggerEnter(Collider other) {
            if (currentUser == null)
            {
                currentUser = other.gameObject;
                currentUserTags = currentUser.GetComponent<TagHolder>();
                if (currentUserTags.IsOverlap(AllowTags)) OnUserEnter?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other?.gameObject == currentUser)
            {
                if (currentUserTags.IsOverlap(AllowTags))
                {
                    OnUserLeave?.Invoke();
                    currentUser = null;
                    currentUserTags = null;
                }
            }
        }
    }
}
