using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    private string tutorialKey = "TutorialShown";

    void Start()
    {
        // Verifica si el tutorial ya ha sido mostrado
        if (!PlayerPrefs.HasKey(tutorialKey))
        {
            // Si no ha sido mostrado, muestra el tutorial
            ShowTutorial();

            // Marca el tutorial como mostrado
            PlayerPrefs.SetInt(tutorialKey, 1);
            PlayerPrefs.Save();
        }
    }

    void ShowTutorial()
    {
        creditsPanel.SetActive(true);

        // Después de un tiempo, oculta el tutorial
        Invoke("HideTutorial", 1f);
    }

    void HideTutorial()
    {
        creditsPanel.SetActive(false);
    }
}
