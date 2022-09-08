using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    [CreateNodeMenu("Texture Generator/Math/Remap Color Values")]

    public class RemapColors : BlitWithInputPort
    {
        [SerializeField, Input()] private float oldMin = 0, oldMax = 1, newMin = 0, newMax = 1;

        public float OldMin
        {
            get => oldMin;
            set => oldMin = value;
        }

        public float OldMax
        {
            get => oldMax;
            set => oldMax = value;
        }

        public float NewMin
        {
            get => newMin;
            set => newMin = value;
        }

        public float NewMax
        {
            get => newMax;
            set => newMax = value;
        }

        private static readonly int NewminID = Shader.PropertyToID("_newmin");
        private static readonly int OldmaxID = Shader.PropertyToID("_oldmax");
        private static readonly int OldminID = Shader.PropertyToID("_oldmin");
        private static readonly int NewmaxID = Shader.PropertyToID("_newmax");

      

        protected override void OnInputChanged()
        {
            PrepareMaterial();
            GetPortValue("oldMin", ref oldMin);
            GetPortValue("oldMax", ref oldMax);
            GetPortValue("newMin", ref newMin);
            GetPortValue("newMax", ref newMax);


            BlitMaterial.SetFloat(OldminID, oldMin);
            BlitMaterial.SetFloat(OldmaxID, oldMax);
            BlitMaterial.SetFloat(NewminID, newMin);
            BlitMaterial.SetFloat(NewmaxID, newMax);
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