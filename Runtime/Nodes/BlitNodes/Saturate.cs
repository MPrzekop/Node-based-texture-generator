using Node_based_texture_generator.Runtime.Nodes.BlitNodes.Base;
using UnityEngine;

namespace Node_based_texture_generator.Runtime.Nodes.BlitNodes
{
    [CreateNodeMenu("Texture Generator/Image Operations/Saturate")]

    public class Saturate : BlitWithInputPort
    {
        protected override void PrepareMaterial()
        {
            if (BlitMaterial == null)
            {
                BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Saturate"));
            }
        }
    }
}