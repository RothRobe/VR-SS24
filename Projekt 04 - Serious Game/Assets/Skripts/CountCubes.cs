using UnityEngine;

public class CountCubes : MonoBehaviour
{
    void Start()
    {
        GameObject wurzel = GameObject.Find("Würfel"); // Finde das Wurzelobjekt "Würfel"
        
        if (wurzel != null)
        {
            int cubeCount = CountCubesInChildren(wurzel); // Zähle die Cubes unterhalb von "Würfel"
            Debug.Log("Anzahl der Cubes: " + cubeCount);
        }
        else
        {
            Debug.Log("Wurzelobjekt 'Würfel' nicht gefunden.");
        }
    }

    // Rekursive Funktion zum Zählen der Cubes in den Kindobjekten
    int CountCubesInChildren(GameObject parent)
    {
        int count = 0;

        // Gehe durch alle direkten Kinder des übergebenen Objekts
        foreach (Transform child in parent.transform)
        {
            // Überprüfe den Namen des Kindobjekts
            if (child.gameObject.name == "Cube")
            {
                count++;
            }

            // Rekursiv alle Kindobjekte dieses Kindobjekts durchsuchen
            count += CountCubesInChildren(child.gameObject);
        }

        return count;
    }
}
