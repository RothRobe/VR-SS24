using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public float growthRate = 0.1f; // Die Geschwindigkeit, mit der der Baum wächst
    public float maxScale = 1f; // Die maximale Skalierung des Baums
    public float fadeDuration = 2f; // Dauer des Ausblendens, nachdem der Baum die maximale Größe erreicht hat

    private bool hasReachedMaxScale = false;
    private float fadeTimer = 0f;
    private Renderer treeRenderer;
    private Color initialColor;

    private TreeSpawner _treeSpawner;

    void Start()
    {
        _treeSpawner = GameObject.Find("/TreeSpawner").GetComponent<TreeSpawner>();
        
        treeRenderer = GetComponent<Renderer>();

        initialColor = treeRenderer.material.color;

        // Setze den Shader auf Transparent
        treeRenderer.material.SetFloat("_Mode", 2);
        treeRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        treeRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        treeRenderer.material.SetInt("_ZWrite", 0);
        treeRenderer.material.DisableKeyword("_ALPHATEST_ON");
        treeRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        treeRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        treeRenderer.material.renderQueue = 3000;
    }

    void Update()
    {
        // Wenn der Baum die maximale Größe noch nicht erreicht hat, skaliere ihn weiter
        if (!hasReachedMaxScale)
        {
            transform.localScale += Vector3.one * (growthRate * Time.deltaTime);

            // Überprüfe, ob der Baum die maximale Größe erreicht hat
            if (transform.localScale.x >= maxScale)
            {
                hasReachedMaxScale = true;
                fadeTimer = fadeDuration;
            }
        }
        else
        {
            // Wenn der Baum die maximale Größe erreicht hat, beginne den Ausblend-Timer
            fadeTimer -= Time.deltaTime;

            // Berechne die neue Alpha-Transparenz
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            Color newColor = initialColor;
            newColor.a = alpha;
            treeRenderer.material.color = newColor;

            // Wenn der Timer abgelaufen ist, deaktiviere oder zerstöre den Baum
            if (fadeTimer <= 0)
            {
                _treeSpawner.RemoveFromList(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
