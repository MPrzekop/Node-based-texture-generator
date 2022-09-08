using System;
using Node_based_texture_generator.Editor.Nodes.Generators.Base;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.Generators
{
    [System.Serializable]
    public enum NoiseType
    {
        White,
        Perlin,
        Voronoi,
        Layered
    }


    [System.Serializable]
    public class Noise : OutputOnlyBlitNode
    {
        [SerializeField] private NoiseType noiseType;
        [SerializeField] private int seed;
        [SerializeField, HideInInspector] private ComputeShader whiteNoiseCompute;

        private INodePortsProvider provider;

        protected override void PrepareMaterial()
        {
            switch (noiseType)
            {
                case NoiseType.White:
                    if (provider == null || provider.GetType() != typeof(WhiteNoiseNodePorts))
                    {
                        provider = new WhiteNoiseNodePorts();
                        provider.PopulatePorts(this);
                    }

                    break;
                case NoiseType.Perlin:
                    if (provider == null || provider.GetType() != typeof(PerlinNoiseNodePorts))
                    {
                        provider = new PerlinNoiseNodePorts();
                        provider.PopulatePorts(this);
                    }

                    BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Perlin"));

                    break;
                case NoiseType.Voronoi:
                    break;
                case NoiseType.Layered:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void RenderToResult(ref RenderTexture result)
        {
            switch (noiseType)
            {
                case NoiseType.White:
                    if (whiteNoiseCompute == null) return;
                    var tempRenderTexture = RenderTexture.GetTemporary(result.descriptor);
                    if (tempRenderTexture.IsCreated())
                        tempRenderTexture.Release();
                    tempRenderTexture.enableRandomWrite = true;
                    tempRenderTexture.Create();
                    whiteNoiseCompute.SetInt("seed", seed);
                    whiteNoiseCompute.SetTexture(0, "Result", tempRenderTexture);
                    whiteNoiseCompute.Dispatch(0, Mathf.CeilToInt(outputResolution.x / 8f),
                        Mathf.CeilToInt(outputResolution.y / 8f), 1);
                    Graphics.Blit(tempRenderTexture, result);
                    RenderTexture.ReleaseTemporary(tempRenderTexture);
                    break;
                case NoiseType.Perlin:
                    PerlinNoiseNodePorts.PerlinPortsValues values =
                        (PerlinNoiseNodePorts.PerlinPortsValues) provider.GetPortsValues(this);
                    BlitMaterial.SetInt("_seed", seed);
                    BlitMaterial.SetVector("_tiling", values.tiling);

                    base.RenderToResult(ref result);
                    break;
                case NoiseType.Voronoi:

                    break;
                case NoiseType.Layered:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}