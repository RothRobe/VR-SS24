using System.Collections.Generic;
using UnityEngine;

public class PedalMovementDetection : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int sampleSize = 20;

    private Queue<Vector3> leftPositions = new Queue<Vector3>();
    private Queue<Vector3> rightPositions = new Queue<Vector3>();

    private OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    private OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    void Update()
    {
        // Positionen der Controller abfragen
        Vector3 leftPosition = OVRInput.GetLocalControllerPosition(leftController);
        Vector3 rightPosition = OVRInput.GetLocalControllerPosition(rightController);
        Debug.Log(leftPosition + "\n" + rightPosition);

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

    void AnalyzeMovement()
    {
        Vector3 leftStart = leftPositions.Peek();
        Vector3 rightStart = rightPositions.Peek();

        Vector3 leftEnd = leftPositions.ToArray()[sampleSize - 1];
        Vector3 rightEnd = rightPositions.ToArray()[sampleSize - 1];

        float leftMovement = (leftEnd.y - leftStart.y);
        float rightMovement = (rightEnd.y - rightStart.y);

        if ((leftMovement > 0 && rightMovement < 0) || (leftMovement < 0 && rightMovement > 0))
        {
            float combinedMovement = Mathf.Abs(leftMovement) + Mathf.Abs(rightMovement);
            Vector3 forwardMovement = transform.forward * combinedMovement * movementSpeed * Time.deltaTime;
            transform.Translate(forwardMovement, Space.World);
        }
    }
}