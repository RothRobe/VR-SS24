using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public TreeSpawner treeList;
    public float speed = 5f;
    public GameObject BirdOrbit;

    private GameObject currentBiggestTree;
    private bool isRotating = false;
    

    // Update is called once per frame
    void Update()
    {
        GameObject biggest = FindBiggestTree();
        if (biggest == null) return;
        if (biggest != currentBiggestTree)
        {
            currentBiggestTree = biggest;
            transform.parent = null;
            isRotating = false;
        }

        if (!isRotating)
        {
            FlyToPosition(currentBiggestTree.transform.position);
        }
    }

    GameObject FindBiggestTree()
    {
        if (treeList.GetTreeList().Count == 0) return null;
        return treeList.GetTreeList().OrderByDescending(go => go.transform.localScale.magnitude).First();
    }
    void FlyToPosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
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
            BirdOrbit.transform.position = position;
            BirdOrbit.transform.rotation = Quaternion.Euler(0f,0f,0f);
            transform.parent = BirdOrbit.transform;
            isRotating = true;
        }
    }
}
