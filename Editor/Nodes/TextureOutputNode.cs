using System.Linq;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes
{
    public class TextureOutputNode : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture texture;


        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("texture").GetInputValue<Texture>();
            if (newInput != texture)
            {
                texture = newInput;
            }

            UpdateTexture();
        }

        public override Texture GetTexture()
        {
            return texture;
        }
    }
}