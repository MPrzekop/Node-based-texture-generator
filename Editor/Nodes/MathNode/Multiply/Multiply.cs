using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using Node_based_texture_generator.Editor.Nodes.MathNode.Multiply.MultiplyBehaviour;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Multiply
{
    [CreateNodeMenu("Texture Generator/Math/Multiply")]
    public class Multiply : MathOperationNode<IMathOperationBehaviour, MultiplyNodeAttribute>
    {
    }
}