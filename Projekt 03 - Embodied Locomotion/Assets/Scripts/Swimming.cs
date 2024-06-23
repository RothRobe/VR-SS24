
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Swimming : MonoBehaviour
{
    public bool isSwimming = false;
    public ArmSwingMovement armSwingMovement;
    public PedalMovementDetection pedalMovementDetection;
    public float movementSpeed = 4f;
    public int sampleSize = 20;
    private Queue<Vector3> leftPositions = new Queue<Vector3>();
    private Queue<Vector3> rightPositions = new Queue<Vector3>();
    private OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    private OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        if (isSwimming){
            // Positionen der Controller abfragen
            Vector3 leftPosition = OVRInput.GetLocalControllerPosition(leftController);
            Vector3 rightPosition = OVRInput.GetLocalControllerPosition(rightController);

            // Neue Positionen hinzufügen
            leftPositions.Enqueue(leftPosition);
            rightPositions.Enqueue(rightPosition);

            // Alte Positionen entfernen, um die Queue auf die gewünschte Samplegröße zu beschränken
            if (leftPositions.Count > sampleSize)
            {
                leftPositions.Dequeue();
                rightPositions.Dequeue();
            }

            // Bewegung analysieren
            if (leftPositions.Count == sampleSize)
            {
                AnalyzeMovement();
            }
        }
        if((transform.position.y < 0.2) && (!pedalMovementDetection.bikeTouched)){
            armSwingMovement.isWalking = false;
            isSwimming = true;
            pedalMovementDetection.isRiding = false;
            rb.useGravity = false;
        }
    }

    void AnalyzeMovement()
    {
        Vector3 leftStart = leftPositions.Peek();
        Vector3 rightStart = rightPositions.Peek();

        Vector3 leftEnd = leftPositions.ToArray()[sampleSize - 1];
        Vector3 rightEnd = rightPositions.ToArray()[sampleSize - 1];

        Vector3 leftMid = leftPositions.ToArray()[sampleSize/2];
        Vector3 rightMid = rightPositions.ToArray()[sampleSize/2];

        float leftMovementX = leftEnd.x - leftStart.x;
        float rightMovementX = rightEnd.x - rightStart.x;

        float leftMovementZ = leftEnd.z - leftStart.z;
        float rightMovementZ = rightEnd.z - rightStart.z;

        if ((leftMovementX > 0 && rightMovementX < 0 && leftMovementZ < 0 && rightMovementZ > 0) 
                || (leftMovementX < 0 && rightMovementX > 0 && leftMovementZ < 0 && rightMovementZ > 0)
                || (leftMovementX > 0 && rightMovementX < 0 && leftMovementZ > 0 && rightMovementZ < 0)
                || (leftMovementX < 0 && rightMovementX > 0 && leftMovementZ > 0 && rightMovementZ < 0))
        {
            float combinedMovement = Mathf.Abs(leftMovementX) + Mathf.Abs(rightMovementX);
            Vector3 forwardMovement = GetHMDForwardDirection() * combinedMovement * movementSpeed * Time.deltaTime;
            /*if(transform.position.y > 0.2){
                forwardMovement.y = 0f;
            }*/
            transform.Translate(forwardMovement, Space.World);
        }
    }

    Vector3 GetHMDForwardDirection()
    {
        // HMD (Head-Mounted Display) abfragen und dessen Blickrichtung als Vorwärtsvektor zurückgeben
        return Camera.main.transform.forward;
    }
}
