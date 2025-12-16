using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteHitboxAdapter : MonoBehaviour
{
    [Header("Configuració")]
    [Range(0f, 1f)]
    public float alphaThreshold = 0.1f;

    public bool autoUpdate = true;

    private Image image;
    private Sprite lastSprite;

    void Start()
    {
        image = GetComponent<Image>();
        AdaptHitbox();
    }

    void Update()
    {
        if (autoUpdate && image.sprite != lastSprite)
        {
            AdaptHitbox();
        }
    }

    public void AdaptHitbox()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (image.sprite == null)
        {
            Debug.LogWarning("L'imatge no conté Sprite");
            return;
        }

        image.alphaHitTestMinimumThreshold = alphaThreshold;

        if (!image.sprite.texture.isReadable)
        {
            Debug.LogWarning($"L'Sprite '{image.sprite.name}' necesaita tenir Read/Write habilitat en les configuracions d'importació");
        }

        lastSprite = image.sprite;
    }
}