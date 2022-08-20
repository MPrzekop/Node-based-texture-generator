using System;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes
{
    public class FileTextureNode : TextureGraphNode
    {
        [SerializeField, Output(ShowBackingValue.Always)]
        private Texture texture;

        public Texture Texture
        {
            get => texture;
            set => texture = value;
        }

        public override Texture GetTexture()
        {
            return Texture;
        }

        protected override void OnInputChanged()
        {
            UpdateTexture();
            UpdateNode(GetPort("texture"));
        }


        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "texture")
            {
                // Return input value + 1
                return this.GetTexture();
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }

        private void OnValidate()
        {
            OnInputChanged();
        }
    }
}