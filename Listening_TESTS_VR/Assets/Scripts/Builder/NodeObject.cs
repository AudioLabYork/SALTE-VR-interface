using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    public int posX;
    public int posZ;
    public int textureID;

    public void UpdateNodeObject(Node curNode, NodeObjectSaveable saveable)
    {
        posX = saveable.posX;
        posZ = saveable.posZ;
        textureID = saveable.textureID;

        ChangeMaterial(curNode);
    }

    private void ChangeMaterial(Node curNode)
    {
        Material getMaterial = LevelEditor.ResourcesManager.GetInstance().GetMaterial(textureID);
        curNode.tileRenderer.material = getMaterial;
    }

    public NodeObjectSaveable GetSaveable()
    {
        NodeObjectSaveable saveable = new NodeObjectSaveable();
        saveable.posX = this.posX;
        saveable.posZ = this.posZ;
        saveable.textureID = this.textureID;

        return saveable;

    }
}

[System.Serializable]
public class NodeObjectSaveable
{
    public int posX;
    public int posZ;
    public int textureID;
}
