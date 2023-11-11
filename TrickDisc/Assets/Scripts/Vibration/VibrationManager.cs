using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private static VibrationManager instance;

    public static VibrationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("VibrationManager").AddComponent<VibrationManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private bool isVibrationEnabled = true;

    public bool IsVibrationEnabled
    {
        get { return isVibrationEnabled; }
        set
        {
            isVibrationEnabled = value;
            PlayerPrefs.SetInt(Constants.DATA.SETTINGS_VIBRATION, isVibrationEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        isVibrationEnabled = PlayerPrefs.GetInt(Constants.DATA.SETTINGS_VIBRATION, 1) == 1;
    }

    public void Vibrate()
    {
        if (isVibrationEnabled && SystemInfo.supportsVibration)
        {
            Handheld.Vibrate();
        }
    }
}
