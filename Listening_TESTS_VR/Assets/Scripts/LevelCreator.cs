using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class LevelCreator : MonoBehaviour
    {

        LevelManager manager;
        GridBase gridBase;
        Interface_manager ui;

        // Place object variables 
        bool hasObj;
        GameObject objToPlace;
        GameObject cloneObj;
        Level_Object objProperties;
        Vector3 mousePosition;
        Vector3 worldPosition;
        bool deleteObj;

        // paint tile variables 
        bool hasMaterial;
        bool paintTile;
        public Material matToPlace;
        Node previousNode;
        Material previousMaterial;
        Quaternion targetRot;

        // wall creator variables
        bool createWall;
        public GameObject wallPrefab;
        Node startNodeWall;
        Node endNodeWall;
        public Material[] wallPlacementMat;
        bool deleteWall;

        private void Start()
        {
            gridBase = GridBase.GetInstance();
            manager = LevelManager.GetInstance();
            ui = Interface_manager.GetInstance();

            PaintAll();
        }

        private void Update()
        {
            PlaceObject();
            PaintTile();
            DeleteObjs();
            PlaceStackedObj();
            CreateWall();
            DeleteStackedObjs();
            DeleteWallObjs();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;
            }

        }


        private void DeleteWallObjs()
        {
            
        }

        private void DeleteStackedObjs()
        {
          
        }

        private void CreateWall()
        {
           
        }

        private void PlaceStackedObj()
        {
            
        }

      

        private void PaintTile()
        {
           
        }



        #region Place and delete Objects
        private void PlaceObject()
        {
            if(hasObj)
            {

                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if(cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPosition, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    cloneObj.transform.position = worldPosition;

                    if(Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                    {
                        if(curNode.placedObject != null)
                        {
                            manager.inSceneTestObjects.Remove(curNode.placedObject.gameObject);
                            Destroy(curNode.placedObject.gameObject);
                            curNode.placedObject = null;
                        }


                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPosition, cloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.placedObject = placedObjProperties;
                        manager.inSceneTestObjects.Add(actualObjPlaced);




                    }
                    if(Input.GetMouseButton(1))
                    {
                        objProperties.ChangeRotation();
                    }
                }
            } else
                if(cloneObj != null)
            {
                Destroy(cloneObj);
            }
           
        }

        public void PassObjectToPlace(string objID)
        {
            if(cloneObj != null)
            {
                Destroy(cloneObj);
            }

            CloseAll();
            hasObj = true;
            cloneObj = null;
            objToPlace = ResourcesManager.GetInstance().GetObjBase(objID).objPrefab;

        }

        private void DeleteObjs()
        {
            if (deleteObj)
            {

                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if(curNode.placedObject != null)
                    {
                        if (manager.inSceneTestObjects.Contains(curNode.placedObject.gameObject))
                        {
                            manager.inSceneTestObjects.Remove(curNode.placedObject.gameObject);
                            Destroy(curNode.placedObject.gameObject);
                        }

                        curNode.placedObject = null;


                    }

                }






            }
        }

        public void DeleteObj()
        {
            CloseAll();
            deleteObj = true;
        }

        #endregion


        private void CloseAll()
        {
            hasObj = false;
            deleteObj = false;
            paintTile = false;
           // placeStackedObj = false;
            createWall = false;
            hasMaterial = false;
            deleteWall = false;
         //   deleteStackObj = false;

        }

        private void PaintAll()
        {
            
        }
    }
}