using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to Control the UI/HealthBar
/// </summary>
public class UIHealthBar : MonoBehaviour
{

    public static UIHealthBar Instance { get; private set; }

    /// <summary>
    /// Singleton Pattern
    /// Create an Instance of the Object
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    public Image mask;
    float originalSize;

    /// <summary>
    /// Sets the originalSize to the width of mask layer
    /// </summary>
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    /// <summary>
    /// Sets the Value of the Healthbar
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
