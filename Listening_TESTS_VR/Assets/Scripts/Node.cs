using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{

    public int nodePosX;
    public int nodePosZ;
    public GameObject vis;
    public MeshRenderer tileRenderer;
    public LevelEditor.Level_Object placedObject;
    public List<LevelEditor.Level_Object> stackedObjs = new List<LevelEditor.Level_Object>();
  //  public LevelEditor.Wall_Obj wallObj;

}
