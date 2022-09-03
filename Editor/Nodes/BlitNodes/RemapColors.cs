using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    [CreateNodeMenu("Texture Generator/Image Operations/Remap Color Values")]

    public class RemapColors : BlitWithInputPort
    {
        [SerializeField, Input()] private float oldMin = 0, oldMax = 1, newMin = 0, newMax = 1;


        private static readonly int Newmin = Shader.PropertyToID("_newmin");
        private static readonly int Oldmax = Shader.PropertyToID("_oldmax");
        private static readonly int Oldmin = Shader.PropertyToID("_oldmin");
        private static readonly int Newmax = Shader.PropertyToID("_newmax");

      

        protected override void OnInputChanged()
        {
            PrepareMaterial();
            GetPortValue("oldMin", ref oldMin);
            GetPortValue("oldMax", ref oldMax);
            GetPortValue("newMin", ref newMin);
            GetPortValue("newMax", ref newMax);


            BlitMaterial.SetFloat(Oldmin, oldMin);
            BlitMaterial.SetFloat(Oldmax, oldMax);
            BlitMaterial.SetFloat(Newmin, newMin);
            BlitMaterial.SetFloat(Newmax, newMax);
            base.OnInputChanged();
        }

        void GetPortValue(string port, ref float val)
        {
            var portVal = GetPort(port);
            if (portVal.IsConnected)
                val = GetPort(port).GetInputValue<float>();
        }

        protected override void PrepareMaterial()
        {
            if (BlitMaterial == null)
            {
                BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Remap"));
            }
        }
    }
}