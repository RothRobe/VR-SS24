using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject bird;
    private GameObject selectedTree;

    public void SetBirdTarget(GameObject tree)
    {
        bird.GetComponent<BirdMovementVR>().SetTargetPosition(tree);
    }

    public void SetSelectedTree(GameObject tree)
    {
        if(selectedTree != null) selectedTree.GetComponent<SelectTree>().Deselect();
        selectedTree = tree;
        SetBirdTarget(tree);
    }
}
