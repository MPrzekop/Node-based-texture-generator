using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.Output;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.GraphBase
{
    [CreateAssetMenu, RequireNode(typeof(TextureOutputNode))]
    public class TextureMainGraph : NodeGraph
    {
        [SerializeField] private string _textureGraphName = "Texture graph";

        public string TextureGraphName
        {
            get => _textureGraphName;
            set => _textureGraphName = value;
        }

        public bool ValidateGraph()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                List<Node> visited = new List<Node>();
                if (IsCyclic(nodes[i], ref visited))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsCyclic(Node currentNode, ref List<Node> visited)
        {
            if (currentNode == null) return false;
            if (visited.Contains(currentNode))
            {
                Debug.Log(currentNode.name + " is cyclic");
                return true;
            }

            visited.Add(currentNode);
            foreach (var output in currentNode.Outputs)
            {
                foreach (var c in output.GetConnections())
                {
                    if (IsCyclic(c.node, ref visited))
                    {
                        Debug.Log(currentNode.name + " is cyclic");
                        return true;
                    }
                }
            }

            visited.Remove(currentNode);

            return false;
        }
    }
}