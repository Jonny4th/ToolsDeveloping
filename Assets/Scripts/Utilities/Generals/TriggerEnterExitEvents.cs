using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolTesting
{
    public class TriggerEnterExitEvents : MonoBehaviour
    {
        [SerializeField] List<ScriptableTags> AllowTags = new List<ScriptableTags>();
        public event Action<GameObject> OnUserEnter;
        public event Action OnUserLeave;
        private GameObject currentUser = null;
        private void OnTriggerEnter(Collider other) {
            if (currentUser == null && other.GetComponent<TagHolder>().IsOverlap(AllowTags))
            {
                currentUser = other.gameObject;
                OnUserEnter?.Invoke(currentUser);
            }
        }
        private void OnTriggerExit(Collider other) {
            if (other.gameObject == currentUser)
            {
                currentUser = null;
                OnUserLeave?.Invoke();
            }
        }
    }
}
