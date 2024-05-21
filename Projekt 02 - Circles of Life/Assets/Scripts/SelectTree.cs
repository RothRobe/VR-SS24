using System;
using UnityEngine;

public class SelectTree : MonoBehaviour
{
    private Outline _outline;
    private Color _color;
    private bool _isActive;
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    
    public Color GetRainbowColor(float t)
    {
        // Umwandlung des Werts t in einen Bereich von 0 bis 1
        t = Mathf.Clamp01(t);

        // Berechnung des Farbverlaufs basierend auf dem Wert t
        float r = Mathf.Clamp01(Mathf.Abs(t * 6f - 3f) - 1f);
        float g = Mathf.Clamp01(2f - Mathf.Abs(t * 6f - 2f));
        float b = Mathf.Clamp01(2f - Mathf.Abs(t * 6f - 4f));

        return new Color(r, g, b);
    }

    void Update()
    {
        if (_isActive)
        {
            // Beispiel: Animation des Regenbogenverlaufs über die Zeit
            float t = Mathf.PingPong(Time.time / 5f, 1f);
            _color = GetRainbowColor(t);

            _outline.OutlineColor = _color;
        }
    }

    public void OnHover()
    {
        _outline.OutlineColor = Color.red;
        _outline.enabled = true;
    }

    public void OnUnhover()
    {
        if (!_isActive) _outline.enabled = false;
    }

    public void Select()
    {
        _isActive = true;
    }

    public void Unselect()
    {
        _isActive = false;
        _outline.OutlineColor = Color.red;
    }
}
