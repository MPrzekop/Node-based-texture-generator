using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes.Base
{
    public abstract class BlitWithInputPort : BlitNodeBase
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited), SerializeField]
        private Texture input;

        protected Texture Input => input;


        protected override Texture GetBlitInputTexture()
        {
            return Input;
        }

        protected override Vector2Int GetOutputResolution()
        {
            if (Input == null)
                return base.GetOutputResolution();
            return new Vector2Int(Input.width, Input.height);
        }


        protected override void OnInputChanged()
        {
            input = GetPort("input").GetInputValue<Texture>();
            base.OnInputChanged();
        }
    }
}