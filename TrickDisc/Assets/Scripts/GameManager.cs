using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private TMP_Text bestScoreText;

    private int score;
    public static int coins;

    [SerializeField] private Animator scoreAnimator;

    [SerializeField] private AnimationClip scoreClip;

    [SerializeField] private Obstacle targetPrefab;

    [SerializeField] private float maxSpawnOffset;

    [SerializeField] private Vector3 startTargetPos;

    [SerializeField] private GameObject endPanel;

    [SerializeField] private Image soundImage;

    [SerializeField] private Sprite activeSoundSprite;

    [SerializeField] private Sprite inactiveSoundSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        endPanel.SetActive(false);

        AudioManager.instance.AddButtonSound();

        score = 0;

        scoreText.text = score.ToString();

        scoreAnimator.Play(scoreClip.name, -1, 0f);

        SpawnObstacle();

        coins = PlayerPrefs.GetInt(Constants.DATA.COINS);
    }

    private void SpawnObstacle()
    {
        Obstacle temp = Instantiate(targetPrefab);
        Vector3 tempPos = startTargetPos;
        startTargetPos.x = Random.Range(-maxSpawnOffset, maxSpawnOffset);
        temp.MoveToPos(tempPos);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.DATA.MAIN_MENU_SCENE);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(Constants.DATA.GAMEPLAY_SCENE);
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

    public void UpdateScore()
    {
        GlobalLogger.Log(score.ToString());
        score++;
        scoreText.text = score.ToString();
        scoreAnimator.Play(scoreClip.name, -1, 0f);
        SpawnObstacle();
    }

    public static void AddScore()
    {
        coins++;
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        endScoreText.text = score.ToString();

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;
        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE) : 0;

        VibrationManager.Instance.Vibrate();

        PlayerPrefs.SetInt(Constants.DATA.COINS, coins);

        GlobalLogger.Log("Loss");

        PlayGamesLogros.UnlockAchievemt("CgkI_b_uzKMWEAIQAg");

        CloudSaveManager.Instance.SaveData(new Dictionary<string, object> { { Constants.DATA.COINS, coins } });

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
            bestScoreText.text = "NEW BEST";
            GlobalLogger.Log("New High Score");
            CloudSaveManager.Instance.SaveData(new Dictionary<string, object> { { Constants.DATA.HIGH_SCORE, score } });
        }
        else
        {
            bestScoreText.text = "BEST " + highScore.ToString();
        }
    }
}
