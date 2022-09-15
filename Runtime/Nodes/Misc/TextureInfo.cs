using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Runtime.Nodes.Base;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes
{    [CreateNodeMenu("Texture Generator/Misc/Texture Info",10000)]

    public class TextureInfo : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture texture;

        [Output(ShowBackingValue.Unconnected), SerializeField]
        private Vector2Int resolution;

        protected override Texture GetPreviewTexture()
        {
            return texture;
        }

        public override object GetValue(NodePort port)
        {
            // Check which output is being requested. 
            // In this node, there aren't any other outputs than "result".
            if (port.fieldName == "resolution")
            {
                // Return input value + 1
                return this.resolution;
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }

        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("texture").GetInputValue<Texture>();
            if (newInput != texture)
            {
                texture = newInput;
                resolution = texture == null ? Vector2Int.zero : new Vector2Int(texture.width, texture.height);
            }

            UpdatePreviewTexture();
            UpdateNode(GetPort("resolution"));
        }
    }
}