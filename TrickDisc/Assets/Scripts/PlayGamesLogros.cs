using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class PlayGamesLogros : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;
    [SerializeField] private string token;

    private void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal async void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //Continue with Play Games Services
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgURL = PlayGamesPlatform.Instance.GetUserImageUrl();

            PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
            {
                Debug.Log("Authorization code: " + code);
                token = code;
            });

            GlobalLogger.Log("Player Id:" + id);

            GlobalLogger.Log("Player Id1:" + AuthenticationService.Instance.PlayerId);

            detailsText.text = "Success \n" + name;
        }
        else
        {
            detailsText.text = "Sign In failed!";
        }
    }

    async Task signInWithGooglePlayGames(string authCode)
    {
        await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);

        SceneManager.LoadScene("PreMenu");
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
