using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using UnityEngine;


[AddNode(typeof(int))]
[AddNode(typeof(float))]
[AddNode(typeof(double))]
[AddNode(typeof(Vector2))]
[AddNode(typeof(Vector3))]
[AddNode(typeof(Vector4))]
[AddNode(typeof(Vector2Int))]
[AddNode(typeof(Vector3Int))]
[AddNode(typeof(string))]
public class BasicAddBehaviour : IAddBehaviour
{
    public object Add(object a, object b)
    {
        dynamic aVal = a;
        dynamic bVal = b;
        return aVal + bVal;
    }
}