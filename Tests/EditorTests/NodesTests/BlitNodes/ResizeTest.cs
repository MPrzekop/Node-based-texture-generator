using Node_based_texture_generator.Editor.Nodes.BlitNodes;
using Node_based_texture_generator.Editor.Utility;
using NUnit.Framework;
using UnityEngine;

namespace Node_based_texture_generator.Tests.EditorTests.NodesTests.BlitNodes
{
    public class ResizeTest
    {
        // A Test behaves as an ordinary method
        [Test(Description = "try to resize non existing texture ")]
        public void ResizeNullResultTest()
        {
            // Use the Assert class to test conditions
            var resolution = new Vector2Int(100, 200);

            RenderTexture result = Utility.ResizeTexture(null, resolution);
            Assert.Null(result);
            result?.Release();
        }

        [Test(Description = "check if resolution has been set")]
        public void ResizeResultResolutionTest()
        {
            // Use the Assert class to test conditions
            var startRes = new Vector2Int(50, 150);
            RenderTexture t = new RenderTexture(startRes.x, startRes.y, 32, RenderTextureFormat.DefaultHDR);
            t.Create();

            var resolution = new Vector2Int(100, 200);
            var result = Utility.ResizeTexture(t, resolution);

            Assert.AreEqual(resolution.x, result.width);
            Assert.AreEqual(resolution.y, result.height);
            Assert.AreEqual(t, result);

            result.Release();
        }

        [Test(Description = "try to set zero resolution")]
        public void ResizeResultZeroResolutionTest()
        {
            // Use the Assert class to test conditions

            var startRes = new Vector2Int(50, 150);
            RenderTexture t = new RenderTexture(startRes.x, startRes.y, 32, RenderTextureFormat.DefaultHDR);
            t.Create();

            var resolution = new Vector2Int(0, 0);
            var result = Utility.ResizeTexture(t, resolution);

            Assert.Greater(result.width, resolution.x);
            Assert.Greater(result.height, resolution.y);
            Assert.AreEqual(t, result);

            result.Release();
        }

        [Test(Description = "try to set negative resolution")]
        public void ResizeResultNegativeResolutionTest()
        {
            // Use the Assert class to test conditions
            var startRes = new Vector2Int(50, 150);
            RenderTexture t = new RenderTexture(startRes.x, startRes.y, 32, RenderTextureFormat.DefaultHDR);
            t.Create();

            var resolution = new Vector2Int(-1, -2);
            var result = Utility.ResizeTexture(t, resolution);

            Assert.Greater(result.width, 0);
            Assert.Greater(result.height, 0);
            Assert.AreEqual(t, result);

            result.Release();
        }

        [Test(Description = "try to set the same resolution")]
        public void ResizeToTheSameResolutionTest()
        {
            // Use the Assert class to test conditions
            var startRes = new Vector2Int(50, 150);
            RenderTexture t = new RenderTexture(startRes.x, startRes.y, 32, RenderTextureFormat.DefaultHDR);
            t.Create();

            var resolution = startRes;
            var result = Utility.ResizeTexture(t, resolution);
            Assert.NotNull(result);
            Assert.AreEqual(resolution.x, result.width);
            Assert.AreEqual(resolution.y, result.height);
            Assert.AreEqual(t, result);
            result.Release();
        }
    }
}