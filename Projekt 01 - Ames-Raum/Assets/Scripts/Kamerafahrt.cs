using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamerafahrt : MonoBehaviour
{
    private Vector3[] positions = new Vector3[]{ 
        new Vector3(3.65f,0.8f,2.82f),
        new Vector3(3.63f,0.8f,-3.9f),
        new Vector3(1.49f,0.8f,-9.1f),
        new Vector3(6f,0.8f,0.7f)};
    private Quaternion[] rotation = new Quaternion[]
    {
        Quaternion.Euler(-0.2f,175.94f,0.003f),
        Quaternion.Euler(-0.2f,175.94f,0.03f),
        Quaternion.Euler(-0.2f,42.55f,0.03f),
        Quaternion.Euler(-0.2f,-136f,0.003f)
    };

    int currentPosition = 0;

    Boolean inBewegung = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if(!inBewegung)
            {
                inBewegung=true;
                StartCoroutine(MoveAround());
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            OVRInput.Controller activeController = OVRInput.GetActiveController();
            if ((activeController & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch)
            {
                GoToNextPosition();
            }
        }
      
    }

    private int nextPosition()
    {
        currentPosition = (currentPosition + 1) % rotation.Length;
        return currentPosition;
    }

    public void GoToNextPosition()
    {
        int i = nextPosition();
        transform.position = positions[i];
        transform.rotation = rotation[i];
    }



    IEnumerator MoveAround()
    {
        float elapsedTime = 0f;
        float moveDuration = 2f;
        for(int i = 0; i < rotation.Length; i++)
        {
            int index = nextPosition();
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = rotation[index];
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = positions[index] + new Vector3(0f, 1f, 0f);
            elapsedTime = 0f;
            

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.Clamp01(elapsedTime / moveDuration));
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, (elapsedTime / (moveDuration * 0.5f)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
            transform.position = targetPosition;
            yield return new WaitForSeconds(5);
        }
        inBewegung = false;
       
    }

}
