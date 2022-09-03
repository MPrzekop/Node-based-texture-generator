using Node_based_texture_generator.Editor.GraphBase.GraphBuilder;
using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Editor.Nodes.BlitNodes;
using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using Node_based_texture_generator.Editor.Utility;
using NUnit.Framework;
using UnityEngine;

namespace Node_based_texture_generator.Tests.EditorTests.NodesTests.BlitNodes
{
    public class BasicPassTest<T> where T: BlitWithInputPort
    {
        [Test]
        public void NullTexturePassTest()
        {
            var graph = GraphBuilder.GetGraph();
            var InputNode = graph.AddNode<FileTexture>();
            InputNode.Texture = null;
            var output = InputNode.GetOutputPort("texture");
            var node = graph.AddNode<T>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            Assert.Null(outputValue);
        }
        
        [Test]
        public void TexturePassTest()
        {
            var graph = GraphBuilder.GetGraph();
            var InputNode = graph.AddNode<FileTexture>();
            InputNode.Texture = Texture2D.whiteTexture;
            var output = InputNode.GetOutputPort("texture");
            var node = graph.AddNode<T>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.NotNull(result);
        }
    }
}
