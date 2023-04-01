using AgentRelated;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(Visitor))]
    public class VisitorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Visitor visitor = (Visitor)target;

            if(GUILayout.Button("Interact with Facility"))
            {
                visitor.Utilize();
            }
        }
    }
}
