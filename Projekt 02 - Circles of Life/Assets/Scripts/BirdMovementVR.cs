using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdMovementVR : MonoBehaviour
{
    public TreeSpawner treeList;
    public float speed = 5f;
    public GameObject BirdOrbit;

    private GameObject currentBiggestTree;
    private bool isRotating = false;
    private bool newPosition = false;
    private Vector3 targetPosition;
    

    // Update is called once per frame
    void Update()
    {
        if (newPosition = true)
        {
            transform.parent = null;
            isRotating = false;
            newPosition = false;
        }

        if (!isRotating)
        {
            FlyToPosition();
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
            BirdOrbit.transform.position = targetPosition;
            BirdOrbit.transform.rotation = Quaternion.Euler(0f,0f,0f);
            transform.parent = BirdOrbit.transform;
            isRotating = true;
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        newPosition = true;
    }
}
