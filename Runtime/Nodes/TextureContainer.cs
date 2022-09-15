using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes
{
    [System.Serializable]
    public class TextureContainer
    {
        [SerializeField, Node.InputAttribute()]
        private Texture texture;

        [SerializeField, Node.InputAttribute()]
        private Vector2Int resolution;

        [SerializeField, Node.InputAttribute()]
        private FilterMode filterMode;
    }
}