using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // Das Prefab des Baums
    public float areaSize = 50f; // Größe der quadratischen Fläche
    public float minDistance = 2f; // Mindestabstand zwischen den Bäumen
    public float minSpawnInterval = 2f; // Mindestzeitintervall zwischen dem Spawnen neuer Bäume
    public float maxSpawnInterval = 5f; // Maximale Zeitintervall zwischen dem Spawnen neuer Bäume
    
    private List<GameObject> trees = new List<GameObject>();

    public List<GameObject> GetTreeList()
    {
        return trees;
    }

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

        if (IsPositionValid(randomPosition))
        {
            GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
            trees.Add(newTree);
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject tree in trees)
        {
            Vector3 treePosition = tree.transform.position;
            if (Vector3.Distance(treePosition, position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    public void RemoveFromList(GameObject tree)
    {
        trees.Remove(tree);
    }

    public bool IsBiggestTree(GameObject tree)
    {
        if (trees.Count == 0) return false;
        return trees.OrderByDescending(go => go.transform.localScale.magnitude).First() == tree;
    }
}
