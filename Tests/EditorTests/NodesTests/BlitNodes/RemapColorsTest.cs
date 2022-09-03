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
    
    
    public class RemapColorsTest : BasicPassTest<RemapColors>
    {
        
        //tests use numbers that are possible to compare 0.25,0.5 etc.
        
        [Test]
        public void RemapRangeMaxTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = Color.white;
            var inputTexture = Utility.GetColoredTexture(textureColor);
            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<RemapColors>();
            node.NewMax = 0.5f;
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0).r, 0.5f);
        }


        [Test]
        public void RemapRangeMinTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = Color.black;
            var inputTexture = Utility.GetColoredTexture(textureColor);
            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<RemapColors>();
            node.NewMin = 0.5f;
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0).r, 0.5f);
        }
        
        [Test]
        public void RemapRangeMinMaxTest()
        {
            var graph = GraphBuilder.GetGraph();
            var inputNode = graph.AddNode<FileTexture>();
            var textureColor = new Color(0,1,0.25f,0.75f);
            var inputTexture = Utility.GetColoredTexture(textureColor);
            Assert.AreEqual(inputTexture.GetPixel(0, 0), textureColor);
            inputNode.Texture = inputTexture;
            var output = inputNode.GetOutputPort("texture");
            var node = graph.AddNode<RemapColors>();
            node.NewMin = 0.25f;
            node.NewMax = 0.75f;
            output.Connect(node.GetInputPort("input"));
            var graphOutput = node.GetOutputPort("output");
            var outputValue = graphOutput.GetOutputValue();
            var result = Utility.ToTexture2D((RenderTexture) outputValue);
            Assert.AreEqual(result.GetPixel(0, 0).r, 0.25f);
            Assert.AreEqual(result.GetPixel(0, 0).g, 0.75f);
            Assert.AreEqual(result.GetPixel(0, 0).b, 0.375f);
            Assert.AreEqual(result.GetPixel(0, 0).a, 0.625f);
        }
    }
}