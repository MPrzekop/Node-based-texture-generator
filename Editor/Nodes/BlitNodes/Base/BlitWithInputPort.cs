using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes.Base
{
    public abstract class BlitWithInputPort : BlitNodeBase
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;

        protected Texture Input => input;


        protected override Texture GetInputTexture()
        {
            return Input;
        }


        protected override void OnInputChanged()
        {
            input = GetPort("input").GetInputValue<Texture>();
            base.OnInputChanged();
        }
    }
}