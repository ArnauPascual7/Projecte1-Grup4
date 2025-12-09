using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteHitboxAdapter : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Umbral de alpha para considerar el píxel como parte del botón (0-1)")]
    [Range(0f, 1f)]
    public float alphaThreshold = 0.1f;

    [Tooltip("Actualizar automáticamente al cambiar el sprite")]
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

    /// <summary>
    /// Adapta la hitbox al sprite actual
    /// </summary>
    public void AdaptHitbox()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (image.sprite == null)
        {
            Debug.LogWarning("No hay sprite asignado al Image component");
            return;
        }

        // Habilitar el modo de prueba de alpha
        image.alphaHitTestMinimumThreshold = alphaThreshold;

        // Asegurarse de que el sprite tiene Read/Write habilitado
        if (!image.sprite.texture.isReadable)
        {
            Debug.LogWarning($"El sprite '{image.sprite.name}' necesita tener Read/Write habilitado en las configuraciones de importación");
        }

        lastSprite = image.sprite;
    }

    /// <summary>
    /// Ajusta el RectTransform al tamaño del sprite
    /// </summary>
    public void FitToSprite()
    {
        if (image == null || image.sprite == null)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect spriteRect = image.sprite.rect;

        rectTransform.sizeDelta = new Vector2(spriteRect.width, spriteRect.height);
    }

    // Método para llamar desde el Inspector
    [ContextMenu("Adaptar Hitbox")]
    void AdaptHitboxMenu()
    {
        AdaptHitbox();
        Debug.Log("Hitbox adaptada al sprite");
    }

    [ContextMenu("Ajustar Tamaño al Sprite")]
    void FitToSpriteMenu()
    {
        FitToSprite();
        Debug.Log("Tamaño ajustado al sprite");
    }
}