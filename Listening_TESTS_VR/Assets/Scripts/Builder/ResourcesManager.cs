using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelEditor
{
    public class ResourcesManager : MonoBehaviour
    {

        public List<LevelGameObjectBase> LevelGameObjects = new List<LevelGameObjectBase>();
        public List<LevelStackableObjsBase> LevelGameObjects_Stacking = new List<LevelStackableObjsBase>();
        public List<Material> LevelMaterials = new List<Material>();
        public GameObject wallPrefab;

        private static ResourcesManager instance = null;
        private void Awake()
        {
            instance = this; 
        }

        public static ResourcesManager GetInstance()
        {
            return instance;


        }


        public LevelGameObjectBase GetObjBase( string objID)
        {

            LevelGameObjectBase retVal = null;

            for(int i = 0; i < LevelGameObjects.Count; i++)
            {
                if(objID.Equals(LevelGameObjects[i].obj_id))
                {
                    retVal = LevelGameObjects[i];
                    break;
                }
            }

            return retVal;

        }

        public LevelStackableObjsBase GetStackableObjBase( string stack_ID)
        {
            LevelStackableObjsBase retVal = null;

            for (int i = 0; i < LevelGameObjects_Stacking.Count; i++)
            {
                if (stack_ID.Equals(LevelGameObjects_Stacking[i].stack_id))
                {
                    retVal = LevelGameObjects_Stacking[i];
                    break;
                }
            }

            return retVal;

        }

        public Material GetMaterial(int matID)
        {

            Material retVal = null;

            for(int i = 0; i < LevelMaterials.Count; i++)
            {
                if(matID == i)
                {
                    retVal = LevelMaterials[i];
                    break;
                }
            }



            return retVal;
        }

        public int GetMaterialID(Material mat)
        {
            int id = -1;

            for(int i = 0; i < LevelMaterials.Count; i++)
            {
                if(mat.Equals(LevelMaterials[i]))
                {
                    id = i;
                    break;
                }
            }

            return id;
        }


    }





    [System.Serializable]
    public class LevelGameObjectBase
    {
        public string obj_id;
        public GameObject objPrefab;
    }

    [System.Serializable]
    public class LevelStackableObjsBase
    {
        public string stack_id;
        public GameObject objPrefab;
    }


}


