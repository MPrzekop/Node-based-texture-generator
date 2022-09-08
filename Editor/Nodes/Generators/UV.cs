using Node_based_texture_generator.Editor.Nodes.Generators.Base;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.Generators
{
    public class UV : OutputOnlyBlitNode
    {
        protected override void PrepareMaterial()
        {
            BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/UVGenerator"));
        }
    }
}