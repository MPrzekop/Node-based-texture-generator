using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.Misc
{
    
    [CreateNodeMenu("Texture Generator/Image Operations/Split Colors")]

    public class Split : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;

        [Output(), SerializeField] private Texture outputR, outputG, outputB, outputA;
        private char[] channels = new[] {'R', 'G', 'B', 'A'};

        protected Texture Input => input;

        protected override Texture GetPreviewTexture()
        {
            return null;
        }

        protected override void OnInputChanged()
        {
            input = GetPort("input").GetInputValue<Texture>();
            UpdatePreviewTexture();
            foreach (var output in Outputs)
            {
                UpdateNode(output);
            }
        }

        public override object GetValue(NodePort port)
        {
            Texture[] t = new[] {outputR, outputG, outputB, outputA};
            for (var index = 0; index < channels.Length; index++)
            {
                var channel = channels[index];
                if (port.fieldName == "output" + channel)
                {
                    return t[index];
                }
            }

            return null;
        }
    }
}