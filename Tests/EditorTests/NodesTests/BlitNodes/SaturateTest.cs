using System.Collections;
using Node_based_texture_generator.Editor.GraphBase.GraphBuilder;
using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Editor.Nodes.BlitNodes;
using Node_based_texture_generator.Editor.Nodes.NodeBuilder;
using Node_based_texture_generator.Editor.Nodes.Output;
using Node_based_texture_generator.Editor.Nodes.ValueNodes;
using Node_based_texture_generator.Editor.Utility;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.TestTools;

namespace Node_based_texture_generator.Tests.EditorTests.NodesTests.BlitNodes
{
    public class SaturateTest : BasicPassTest<Saturate>
    {
        // A Test behaves as an ordinary method


        [Test]
        public void TextureInRangeColorPassTest()
        {
            var graph = GraphBuilder.GetGraph();
            var InputNode = graph.AddNode<FileTexture>();
            InputNode.Texture = Texture2D.whiteTexture;
            var output = InputNode.GetOutputPort("texture");
            var node = graph.AddNode<Saturate>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(Color.white, result.GetPixel(0, 0));
        }

        [Test]
        public void NegativeValuesTexturePassTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = new Color(-1, -1, -1, 1);
            var inputTexture = Utility.GetColoredTexture(textureColor);


            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<Saturate>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0), Color.black);
        }

        [Test]
        public void Over1ValuesTexturePassTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = new Color(2, 2, 2, 1);
            var inputTexture = Utility.GetColoredTexture(textureColor);
            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<Saturate>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0), Color.white);
        }
    }
}