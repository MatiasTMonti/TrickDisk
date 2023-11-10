using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite activeSoundSprite;
    [SerializeField] private Sprite inactiveSoundSprite;

    private void Start()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTTINGS_SOUND)
            : 1) == 1;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

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
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTTINGS_SOUND)
            : 1) == 1;

        sound = !sound;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        PlayerPrefs.SetInt(Constants.DATA.SETTTINGS_SOUND, sound ? 1 : 0);

        AudioManager.instance.ToggleSound();
    }
}
