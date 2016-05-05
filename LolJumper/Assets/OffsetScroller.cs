using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{

    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        savedOffset = _renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}