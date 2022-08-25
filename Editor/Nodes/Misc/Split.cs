using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes;
using UnityEngine;
using XNode;

public class Split : TextureGraphNode
{
    [Input(connectionType = ConnectionType.Override), SerializeField]
    private Texture input;

    [Output(), SerializeField] private Texture outputR, outputG, outputB, outputA;
    private char[] channels = new[] {'R', 'G', 'B', 'A'};

    protected Texture Input => input;

    public override Texture GetTexture()
    {
        return null;
    }

    protected override void OnInputChanged()
    {
        input = GetPort("input").GetInputValue<Texture>();
        UpdateTexture();
        foreach (var output in Outputs)
        {
            UpdateNode(output);
        }
    }

    public override object GetValue(NodePort port)
    {
        Texture[] t = new[] {outputR, outputG, outputB, outputA};
        for (var index = 0; index < channels.Length; index++)
        {
            var channel = channels[index];
            if (port.fieldName == "output" + channel)
            {
                return t[index];
            }
        }

        return null;
    }
}