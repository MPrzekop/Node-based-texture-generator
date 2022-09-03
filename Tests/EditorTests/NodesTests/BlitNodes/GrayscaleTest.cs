using System.Collections;
using Node_based_texture_generator.Editor.GraphBase.GraphBuilder;
using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Editor.Nodes.BlitNodes;
using Node_based_texture_generator.Editor.Utility;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Node_based_texture_generator.Tests.EditorTests.NodesTests.BlitNodes
{
    public class GrayscaleTest : BasicPassTest<Grayscale>
    {
        [Test]
        public void IsImageGrayscaleTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = new Color(0.1f, 0.2f, 0.3f, 1f);
            var inputTexture = Utility.GetColoredTexture(textureColor);


//            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<Grayscale>();
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0).r, result.GetPixel(0, 0).g);
            Assert.AreEqual(result.GetPixel(0, 0).r, result.GetPixel(0, 0).b);
        }
    }
}