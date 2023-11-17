using UnityEngine;
using UnityEditor;

public class PlayerPrefsTool : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reset successfully.");
    }

    [MenuItem("Tools/PlayerPrefs Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsTool), false, "PlayerPrefs Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Tool", EditorStyles.boldLabel);

        if (GUILayout.Button("Reset PlayerPrefs"))
        {
            ResetPlayerPrefs();
        }
    }
#endif
}
