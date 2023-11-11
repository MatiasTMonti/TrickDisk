using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{
    [SerializeField] private Toggle vibrationToggle;

    private void Start()
    {
        // Asigna la funci�n ToggleVibration al evento del Toggle
        vibrationToggle.onValueChanged.AddListener(ToggleVibration);

        // Establece el estado inicial del Toggle seg�n la preferencia almacenada
        vibrationToggle.isOn = VibrationManager.Instance.IsVibrationEnabled;
    }

    private void ToggleVibration(bool isVibrationEnabled)
    {
        VibrationManager.Instance.IsVibrationEnabled = isVibrationEnabled;
    }
}
