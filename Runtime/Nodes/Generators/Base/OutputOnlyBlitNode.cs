using Node_based_texture_generator.Runtime.Nodes.BlitNodes.Base;
using UnityEngine;

namespace Node_based_texture_generator.Runtime.Nodes.Generators.Base
{
    public abstract class OutputOnlyBlitNode : BlitNodeBase
    {
        [SerializeField, Input] protected Vector2Int outputResolution;
        // Start is called before the first frame update
        protected override Vector2Int GetOutputResolution()
        {
            if (outputResolution.x <= 0) outputResolution.x = 1;
            if (outputResolution.y <= 0) outputResolution.y = 1;
            return outputResolution;
        }
    
        protected override void OnInputChanged()
        {
            GetPortValue(ref outputResolution, "outputResolution");


            base.OnInputChanged();
        }

    
    }
}
