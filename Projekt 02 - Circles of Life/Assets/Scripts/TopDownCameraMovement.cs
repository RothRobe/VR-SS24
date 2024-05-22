using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
    public GameObject bird;
    
    void Update()
    {
        transform.position = bird.transform.position + new Vector3(0f, 13f, 0f);
    }
}
