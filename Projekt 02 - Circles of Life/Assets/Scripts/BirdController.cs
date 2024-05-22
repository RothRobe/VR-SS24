using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject bird;
    private GameObject selectedTree;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBirdTarget(Vector3 position)
    {
        bird.GetComponent<BirdMovementVR>().SetTargetPosition(position);
    }

    public void SetSelectedTree(GameObject tree)
    {
        if(selectedTree != null) selectedTree.GetComponent<SelectTree>().Deselect();
        selectedTree = tree;
        SetBirdTarget(tree.transform.position);
    }
}
