namespace Node_based_texture_generator.Editor.Nodes.MathNode.Multiply.MultiplyBehaviour
{
    public interface IMultiplyBehaviour
    {
        object Perform(object a, object b);
    }

    public interface IMathOperationBehaviour
    {
        object Perform(object a, object b);
    }
}