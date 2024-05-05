using System.Collections;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(6.964f, 0.152f, -5.961f); // Startposition des Objekts
    private Vector3 endPosition = new Vector3(-0.45f, -0.52f, -9.71f); // Endposition des Objekts
    public float moveDuration = 0.5f; // Dauer der Bewegung in Sekunden

    private float elapsedTime = 0f;
    private bool isRotating = false;
    private bool goingright = true;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / moveDuration); // Wert zwischen 0 und 1 für die Interpolation

        // Interpolation zwischen Start- und Endposition
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        if(!isRotating && t >= 0.95f)
        {
            StartCoroutine(RotateObject());
        }

        if (t >= 1f)
        {
            // Bewegung abgeschlossen, hier können Sie weitere Aktionen ausführen
            isRotating = false;
            var temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
            elapsedTime = 0f;

        }
    }
    IEnumerator RotateObject()
    {
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation;
        if(goingright)
        {
            targetRotation = Quaternion.Euler(0f, 90f, 0f);
            goingright = false;
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
            goingright = true;
        }

        while (elapsedTime < moveDuration * 0.05f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, (elapsedTime / (moveDuration * 0.05f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }

}