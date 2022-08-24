using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Node_based_texture_generator.Editor.Nodes.Output.Editor
{
    [CustomNodeEditor(typeof(TextureOutputNode))]
    public class TextureOutputEditor : TextureNodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            GUILayout.Space(20);
            if (GUILayout.Button("Save"))
            {
                ((TextureOutputNode) target).SaveTexture();
            }
        }
    }
}