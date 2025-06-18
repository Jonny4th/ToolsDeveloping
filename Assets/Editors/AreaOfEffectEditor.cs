using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(AreaOfEffectController))]
    public class AreaOfEffectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AreaOfEffectController controller = (AreaOfEffectController)target;

            if(GUILayout.Button("Activate"))
            {
                controller.TriggerAreaOfEffect();
            }
        }
    }
}