using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Node_based_texture_generator.Editor.Nodes
{
    [NodeEditor.CustomNodeEditorAttribute(typeof(TextureGraphNode))]
    public class TextureNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
                base.OnBodyGUI();


            var r = EditorGUILayout.GetControlRect(GUILayout.Height(GetWidth() - 20));

            EditorGUILayout.BeginVertical();
            var t = ((TextureGraphNode) target).ResultTexture;


            if (t != null)
            {
                EditorGUI.DrawPreviewTexture(r, t);
            }

            EditorGUILayout.EndVertical();
        }

        private Texture2D TextureField(string name, Texture2D texture, int width = 70)
        {
            GUILayout.BeginVertical();

            var result = (Texture2D) EditorGUILayout.ObjectField(texture, typeof(Texture2D), false,
                GUILayout.Width(width), GUILayout.Height(width));
            GUILayout.EndVertical();
            return result;
        }
    }
}