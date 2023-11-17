using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    void Start()
    {
        if (!PlayerPrefs.HasKey(Constants.DATA.TUTORIAL_KEY))
        {
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        creditsPanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideTutorial();
            PlayerPrefs.SetInt(Constants.DATA.TUTORIAL_KEY, 1);
            PlayerPrefs.Save();
        }
    }

    void HideTutorial()
    {
        creditsPanel.SetActive(false);
    }
}
