
using UnityEngine;

namespace Node_based_texture_generator.Runtime.Nodes.MathNode.Multiply.MultiplyBehaviour
{
    [MultiplyNode(typeof(int))]
    [MultiplyNode(typeof(float))]
    [MultiplyNode(typeof(double))]
    [MultiplyNode(typeof(Vector2), typeof(int), false)]
    [MultiplyNode(typeof(Vector3), typeof(int), false)]
    [MultiplyNode(typeof(Vector4), typeof(int), false)]
    [MultiplyNode(typeof(Vector2Int), typeof(int), false)]
    [MultiplyNode(typeof(Vector3Int), typeof(int), false)]
    [MultiplyNode(typeof(Vector2), typeof(float), false)]
    [MultiplyNode(typeof(Vector3), typeof(float), false)]
    [MultiplyNode(typeof(Vector4), typeof(float), false)]
    public class BasicMultiplyBehaviour : IMathOperationBehaviour
    {
        public object Perform(object a, object b)
        {
            dynamic aVal = a;
            dynamic bVal = b;


            return aVal * bVal;
        }
    }
}