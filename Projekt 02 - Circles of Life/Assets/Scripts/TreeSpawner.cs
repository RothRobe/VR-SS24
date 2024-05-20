using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // Das Prefab des Baums
    public float areaSize = 50f; // Größe der quadratischen Fläche
    public float minDistance = 2f; // Mindestabstand zwischen den Bäumen
    public float minSpawnInterval = 2f; // Mindestzeitintervall zwischen dem Spawnen neuer Bäume
    public float maxSpawnInterval = 5f; // Maximale Zeitintervall zwischen dem Spawnen neuer Bäume

    private List<Vector3> treePositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(SpawnTreeCoroutine());
    }

    IEnumerator SpawnTreeCoroutine()
    {
        while (true)
        {
            SpawnTree();

            // Warten für ein zufälliges Zeitintervall zwischen minSpawnInterval und maxSpawnInterval
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    void SpawnTree()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-areaSize / 2, areaSize / 2),
            0,
            Random.Range(-areaSize / 2, areaSize / 2)
        );

        while (!IsPositionValid(randomPosition))
        {
            randomPosition = new Vector3(
                Random.Range(-areaSize / 2, areaSize / 2),
                0,
                Random.Range(-areaSize / 2, areaSize / 2)
            );
        }
        GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
        treePositions.Add(randomPosition);

        // Entferne die Position aus der Liste, nachdem der Baum verschwindet
        TreeGrowth treeGrowth = newTree.GetComponent<TreeGrowth>();
        if (treeGrowth != null)
        {
            StartCoroutine(RemoveTreePositionAfterFade(treeGrowth.fadeDuration, randomPosition));
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 treePosition in treePositions)
        {
            if (Vector3.Distance(treePosition, position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator RemoveTreePositionAfterFade(float fadeDuration, Vector3 position)
    {
        // Warte, bis der Baum vollständig verblasst ist
        yield return new WaitForSeconds(fadeDuration);
        treePositions.Remove(position);
    }
}
