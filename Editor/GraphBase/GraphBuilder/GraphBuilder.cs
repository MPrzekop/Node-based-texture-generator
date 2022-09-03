using UnityEngine;

namespace Node_based_texture_generator.Editor.GraphBase.GraphBuilder
{
    public static class GraphBuilder
    {
        public static TextureMainGraph GetGraph()
        {
            return ScriptableObject.CreateInstance<TextureMainGraph>();
        }
    }
}