using UnityEngine;

public class ReflectionController : MonoBehaviour
{
    public Transform spriteAnchor;
    public SpriteRenderer reflectSprite;
    Color _refColor;

    private void Start()
    {
        _refColor = reflectSprite.color;
    }

    public void UpdateSpriteAlpha(float delta)
    {
        reflectSprite.color = new Color(_refColor.r, _refColor.g, _refColor.b, delta);
    }
}
