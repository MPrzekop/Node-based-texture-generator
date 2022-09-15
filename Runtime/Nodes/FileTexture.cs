using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Runtime.Nodes.Base;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes
{
    [CreateNodeMenu("Texture Generator/Values/Texture")]
    public class FileTexture : TextureGraphNode
    {
        [SerializeField, Output(ShowBackingValue.Always)]
        private Texture texture;

        public Texture Texture
        {
            get => texture;
            set => texture = value;
        }

        protected override Texture GetPreviewTexture()
        {
            return Texture;
        }

        protected override void OnInputChanged()
        {
            UpdatePreviewTexture();
            UpdateNode(GetPort("texture"));
        }


        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "texture")
            {
                // Return input value + 1
                return this.GetPreviewTexture();
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }
    }
}