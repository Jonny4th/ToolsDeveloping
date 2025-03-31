using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(MetronomeController))]
    public class MetronomeControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MetronomeController controller = (MetronomeController)target;

            if(GUILayout.Button("Activate"))
            {
                controller.StartMetronome();
            }
            
            if(GUILayout.Button("Deactivate"))
            {
                controller.StopMetronome();
            }
        }
    }
}
