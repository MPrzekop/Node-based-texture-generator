using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.GraphBase
{
    [CreateAssetMenu, RequireNode(typeof(TextureOutputNode))]
    public class TextureMainGraph : NodeGraph
    {
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
            if (visited.Contains(currentNode)) return true;
            visited.Add(currentNode);
            foreach (var output in currentNode.Outputs)
            {
                foreach (var c in output.GetConnections())
                {
                    if (IsCyclic(c.node, ref visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}