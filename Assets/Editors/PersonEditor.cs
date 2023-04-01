using AgentRelated;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(Person))]
    public class PersonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Person person = (Person)target;

            if(GUILayout.Button("Interact with Facility"))
            {
                person.Utilize();
            }
        }
    }
}
