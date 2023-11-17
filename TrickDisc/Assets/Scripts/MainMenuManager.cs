using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite activeSoundSprite;
    [SerializeField] private Sprite inactiveSoundSprite;
    [SerializeField] private Image vibracionImage;
    [SerializeField] private Sprite vibracionActivadaSprite;
    [SerializeField] private Sprite vibracionDesactivadaSprite;

    private void Start()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        bool vibracionActivada = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_VIBRATION)
            ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_VIBRATION) : 1) == 1;

        vibracionImage.sprite = vibracionActivada ? vibracionActivadaSprite : vibracionDesactivadaSprite;


        AudioManager.instance.AddButtonSound();
    }

    public void ClickedPlay()
    {
        SceneManager.LoadScene(Constants.DATA.GAMEPLAY_SCENE);
    }

    public void ClickedQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;

        sound = !sound;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);

        AudioManager.instance.ToggleSound();
    }

    public void ToggleVibration()
    {
        bool vibracionActivada = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_VIBRATION)
        ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_VIBRATION) : 1) == 1;

        // Invierte el estado de la vibración
        vibracionActivada = !vibracionActivada;

        // Configura la imagen según el nuevo estado de la vibración
        vibracionImage.sprite = vibracionActivada ? vibracionActivadaSprite : vibracionDesactivadaSprite;

        // Guarda la configuración de la vibración
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_VIBRATION, vibracionActivada ? 1 : 0);

        // Actualiza la configuración de la vibración en VibrationManager
        VibrationManager.Instance.IsVibrationEnabled = vibracionActivada;
    }
}
