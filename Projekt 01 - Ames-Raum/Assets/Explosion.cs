using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject broken_guckloch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Explodable"))
        {
            Debug.Log("Triggered Explodable");
            GameObject broken = Instantiate(broken_guckloch);
            Rigidbody[] rigidbodies = broken.GetComponentsInChildren<Rigidbody>();
            Destroy(other.transform.parent.gameObject);
            Debug.Log(other.relativeVelocity.magnitude);
            foreach(Rigidbody rb in rigidbodies)
            {
                rb.AddExplosionForce(200f,other.contacts[0].point,2);
            }
            StartCoroutine(DestroyObjectAfterDelay(broken));
        }
    }
    
    IEnumerator DestroyObjectAfterDelay(GameObject go)
    {
        yield return new WaitForSeconds(2);
        
        Destroy(go);
    }
}
