using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRotation : MonoBehaviour
{
    [SerializeField][Range(-180,180)]
    private float _DegreesPerSecond;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, _DegreesPerSecond, 0) * Time.deltaTime);
    }
}
