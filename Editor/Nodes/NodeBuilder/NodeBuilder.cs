using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.NodeBuilder
{
    public static class NodeBuilder<T>
        where T : TextureGraphNode
    {
        public static T GetNode()
        {
            return ScriptableObject.CreateInstance<T>();
        }
    }
}