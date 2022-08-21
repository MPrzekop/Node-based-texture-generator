using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    public class Vector4ValueInput : TextureGraphNode
    {
        [Output(ShowBackingValue.Always), SerializeField]
        private Vector4 value;

        public override Texture GetTexture()
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
            UpdateTexture();
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