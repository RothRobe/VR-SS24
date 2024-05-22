using System;
using OVR.OpenVR;
using UnityEngine;

public class SelectTree : MonoBehaviour
{
    private Outline _outline;
    private Color _color;
    private BirdController _controller;
    private bool _isSelected;
    private void Start()
    {
        _controller = GameObject.Find("/Controller").GetComponent<BirdController>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void OnHover()
    {
        if (!_isSelected)
        {
            _outline.OutlineColor = new Color(1f, 0.647f, 0f);
            _outline.enabled = true;
        }
    }

    public void OnUnhover()
    {
        if (!_isSelected)
        {
            _outline.enabled = false;
        }
    }

    public void Select()
    {
        _isSelected = true;
        _controller.SetSelectedTree(gameObject);
        _outline.OutlineColor = Color.red;
        _outline.enabled = true;
    }

    public void Deselect()
    {
        _isSelected = false;
        _outline.enabled = false;
    }
}
