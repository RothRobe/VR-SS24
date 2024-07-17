using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementConstraint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.localPosition;

        if (currentPosition.y < -1.167878f)
        {
            transform.localPosition = new Vector3(currentPosition.x, -1.167878f, currentPosition.z);
        }
    }
}
