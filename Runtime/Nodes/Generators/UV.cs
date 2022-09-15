using Node_based_texture_generator.Runtime.Nodes.Generators.Base;
using UnityEngine;

namespace Node_based_texture_generator.Runtime.Nodes.Generators
{
    [CreateNodeMenu("Texture Generator/Generators/UV texture")]
    public class UV : OutputOnlyBlitNode
    {
        protected override void PrepareMaterial()
        {
            BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/UVGenerator"));
        }
    }
}