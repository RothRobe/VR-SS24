using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamerafahrt : MonoBehaviour
{
    private Vector3 move = new Vector3(0.3f, 0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")){
            transform.position = transform.position + move * Time.deltaTime;
        }
    }
}
