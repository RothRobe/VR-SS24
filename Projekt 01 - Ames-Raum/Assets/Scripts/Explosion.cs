using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject broken_guckloch;
    public GameObject particles;
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
            GetComponent<AudioSource>().Play();
            GetComponent<Renderer>().enabled = false;
            foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
            GameObject broken = Instantiate(broken_guckloch);
            Rigidbody[] rigidbodies = broken.GetComponentsInChildren<Rigidbody>();
            Destroy(other.transform.parent.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);
            foreach(Rigidbody rb in rigidbodies)
            {
                rb.AddExplosionForce(200f,other.contacts[0].point,2);
            }
            StartCoroutine(DestroyObjectAfterDelay(broken));

        }
    }
    
    IEnumerator DestroyObjectAfterDelay(GameObject go)
    {
        float elapsedTime = 0f;
        float disappearanceDuration = 1f;
        Color originalColor = go.transform.GetComponentInChildren<Renderer>().material.color;

        while (elapsedTime < disappearanceDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / disappearanceDuration);

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            
            foreach(Transform child in go.transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().material.color = newColor;
            }
            

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        foreach (Transform child in go.transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        yield return new WaitForSeconds(4);

        Destroy(go);
        Destroy(gameObject);
    }
}
