using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayGamesLogros : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;

    private void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //Continue with Play Games Services
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgURL = PlayGamesPlatform.Instance.GetUserImageUrl();

            detailsText.text = "Success \n" + name;

            SceneManager.LoadScene("PreMenu");
        }
        else
        {
            detailsText.text = "Sign In failed!";
        }
    }

    public void LoginFailed()
    {
        SceneManager.LoadScene("PreMenu");
    }

    #region ACHIEVEMENTS
    public static void UnlockAchievemt(string id)
    {
        PlayGamesPlatform.Instance.ReportProgress(id, 100, success => { });
    }

    public static void ShowAchievementsUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    #endregion
}
