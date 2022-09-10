using UnityEngine;


public class HealthbarUI : MonoBehaviour
{
    [Header("Choose image inside the canvas")]
    [SerializeField] private RectTransform _healthbarImage;


    /// <summary>
    /// Update UI Healthbar
    /// </summary>
    /// <param name="t">[0..1] scale percentage factor</param>
    public void HealthChanged(float t)
    {
        Vector3 currentScale = _healthbarImage.localScale;
        _healthbarImage.localScale = new Vector3(t, currentScale.y, currentScale.z);
    }
}
