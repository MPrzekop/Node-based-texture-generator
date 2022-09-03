using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    
    [CreateNodeMenu("Texture Generator/Values/Vector 4")]

    public class Vector4ValueInput : TextureGraphNode
    {
        [Output(ShowBackingValue.Always), SerializeField]
        private Vector4 value;

        protected override Texture GetPreviewTexture()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(1, 1, value);
            tex.Apply();
            return tex;
        }

        private void OnValidate()
        {
            OnInputChanged();
        }

        protected override void OnInputChanged()
        {
            UpdatePreviewTexture();
            UpdateNode(GetPort("value"));
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "value")
            {
                return value;
            }

            return null;
        }
    }
}