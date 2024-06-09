using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PedalMovementDetection : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int sampleSize = 20;
    public ArmSwingMovement armSwingMovement;
    public Swimming swimming;
    public bool isRiding = false;

    public bool bikeTouched = false;
    public Vector3 bikePosition;
    public Vector3 myPosition;
    public GameObject bike;
    public OVRCameraRig mainCamera;
    Rigidbody rb;

    private Queue<Vector3> leftPositions = new Queue<Vector3>();
    private Queue<Vector3> rightPositions = new Queue<Vector3>();

    private OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    private OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    
    void Update()
    {
        bikePosition = bike.transform.position;
        myPosition = mainCamera.transform.position;
        if((Mathf.Abs(bikePosition.x - myPosition.x) <= 0.5) && (Mathf.Abs(bikePosition.z - myPosition.z) <= 0.5)){
            bikeTouched = true;
        }else{
            bikeTouched = false;
        }
        if(isRiding){
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

        if(bikeTouched){
            armSwingMovement.isWalking = false;
            swimming.isSwimming = false;
            isRiding = true;
        }else{
            armSwingMovement.isWalking = true;
            isRiding = false;

        }
    }

    void AnalyzeMovement()
    {
        Vector3 leftStart = leftPositions.Peek();
        Vector3 rightStart = rightPositions.Peek();

        Vector3 leftEnd = leftPositions.ToArray()[sampleSize - 1];
        Vector3 rightEnd = rightPositions.ToArray()[sampleSize - 1];

        float leftMovementY = leftEnd.y - leftStart.y;
        float rightMovementY = rightEnd.y - rightStart.y;

        float leftMovementX = leftEnd.x - leftStart.x;
        float rightMovementX = rightEnd.x - rightStart.x;

        float leftMovementZ = leftEnd.z - leftStart.z;
        float rightMovementZ = rightEnd.z - rightStart.z;

        if (((leftMovementY > 0 && rightMovementY < 0) || (leftMovementY < 0 && rightMovementY > 0)) &&
             ((leftMovementX > 0 && rightMovementX < 0) || (leftMovementX < 0 && rightMovementX > 0)) ||
             ((leftMovementZ > 0 && rightMovementZ < 0) || (leftMovementZ < 0 && rightMovementZ > 0)))
        {
            float combinedMovement = Mathf.Abs(leftMovementY) + Mathf.Abs(rightMovementY);
            Vector3 forwardMovement = GetHMDForwardDirection() * combinedMovement * movementSpeed * Time.deltaTime;
            // Nur X- und Z-Koordinaten verwenden
            forwardMovement.y = 0f;
            transform.Translate(forwardMovement, Space.World);
            bike.transform.Translate(forwardMovement, Space.World);
        }

        Vector3 GetHMDForwardDirection()
    {
        // HMD (Head-Mounted Display) abfragen und dessen Blickrichtung als Vorwärtsvektor zurückgeben
        return Camera.main.transform.forward;
    }
    }
}