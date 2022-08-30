using UnityEngine;


namespace Node_based_texture_generator.Editor.Nodes.MathNode.Multiply.MultiplyBehaviour
{
    [MultiplyNode(typeof(Vector2))]
    [MultiplyNode(typeof(Vector3))]
    [MultiplyNode(typeof(Vector4))]
    [MultiplyNode(typeof(Vector2Int))]
    [MultiplyNode(typeof(Vector3Int))]
    public class VectorMultiplyBehaviour : IMathOperationBehaviour
    {
        public object Perform(object a, object b)
        {
            Vector4 aContainer = ParseValue(a);
            Vector4 bContainer = ParseValue(b);
            var returnValue = new Vector4();
            for (int i = 0; i < 4; i++)
            {
                returnValue[i] = aContainer[i] * bContainer[i];
            }
            return returnValue;
        }

        Vector4 ParseValue(object value)
        {
            Vector4 result = new Vector4();
            ParseValueVector2Int(value, ref result);
            ParseValueVector3Int(value, ref result);
            ParseValueVector2(value, ref result);
            ParseValueVector3(value, ref result);
            ParseValueVector4(value, ref result);
            return result;
        }

        void ParseValueVector2Int(object value, ref Vector4 result)
        {
            if (value is Vector2Int vI)
            {
                result = new Vector4(vI.x, vI.y);
            }
        }

        void ParseValueVector3Int(object value, ref Vector4 result)
        {
            if (value is Vector3Int vI)
            {
                result = new Vector4(vI.x, vI.y, vI.z);
            }
        }

        void ParseValueVector2(object value, ref Vector4 result)
        {
            if (value is Vector2 vI)
            {
                result = vI;
            }
        }

        void ParseValueVector3(object value, ref Vector4 result)
        {
            if (value is Vector3 vI)
            {
                result = vI;
            }
        }

        void ParseValueVector4(object value, ref Vector4 result)
        {
            if (value is Vector4 vI)
            {
                result = vI;
            }
        }
    }
}