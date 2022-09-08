using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes;
using UnityEngine;
using XNode;

public abstract class GenericValueNode<T> : TextureGraphNode
{
    [Node.OutputAttribute(Node.ShowBackingValue.Always), SerializeField]
    protected T value;
    // Start is called before the first frame update
    

    protected override void OnInputChanged()
    {
        UpdatePreviewTexture();
        UpdateNode(GetPort("value"));
    }

    public override object GetValue(NodePort port)
    {
        if (port.fieldName == "value")
        {
            return value;
        }

        return null;
    }
}
