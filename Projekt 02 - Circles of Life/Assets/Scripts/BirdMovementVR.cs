using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdMovementVR : MonoBehaviour
{
    public TreeSpawner treeList;
    public float speed = 5f;
    public GameObject BirdOrbit;

    private GameObject currentTree;
    private bool isRotating = false;
    private bool newPosition = false;
    private Vector3 targetPosition;
    private GameController _gameController;
    private Vector3 offset;
    private bool updatedScore;

    private void Start()
    {
        _gameController = GameObject.Find("/Controller").GetComponent<GameController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (newPosition)
        {
            transform.parent = null;
            isRotating = false;
            newPosition = false;
            updatedScore = false;
        }

        if (!isRotating)
        {
            FlyToPosition();
        }
        else
        {
            Rotate();
        }
    }

    GameObject FindBiggestTree()
    {
        if (treeList.GetTreeList().Count == 0) return null;
        return treeList.GetTreeList().OrderByDescending(go => go.transform.localScale.magnitude).First();
    }
    void FlyToPosition()
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; 


        float distance = direction.magnitude;
        BirdOrbit.transform.position = new Vector3(10f, 0, -15f);

     
        if (distance > 5f)
        {
            
            direction = direction.normalized;

           
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
            
            transform.position += direction * (speed * Time.deltaTime); //Klammerung eigentlich egal, aber laut meiner IDE effizienter??? I guess.
        }
        else
        {
            //Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,140f,0f), Time.deltaTime * speed);
            /*BirdOrbit.transform.position = targetPosition;
            BirdOrbit.transform.rotation = Quaternion.Euler(0f,0f,0f);
            transform.parent = BirdOrbit.transform;
            transform.rotation = Quaternion.Euler(0,140f,0);*/
            offset = (transform.position - targetPosition).normalized * 5f;
            transform.position = targetPosition + offset;
            isRotating = true;
        }
    }

    public void SetTargetPosition(GameObject tree)
    {
        currentTree = tree;
        targetPosition = tree.transform.position;
        newPosition = true;
    }

    public void Rotate()
    {
        // Orbit-Bewegung berechnen
        float angle = 50f * Time.deltaTime;
        offset = Quaternion.Euler(0, angle, 0) * offset;
        transform.position = targetPosition + offset;

        // Richtung des Vogels anpassen
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.rotation =  Quaternion.Euler(0f,Quaternion.LookRotation(-direction).eulerAngles.y,0f) * Quaternion.Euler(0,90,0);

        if (currentTree == null) return;
        if (!updatedScore && treeList.IsBiggestTree(currentTree))
        {
            updatedScore = true;
            _gameController.UpdateScoreboard();
        }
    }
}
