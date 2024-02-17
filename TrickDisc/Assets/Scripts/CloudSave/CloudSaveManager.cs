using System.Collections.Generic;
using UnityEngine;

using Unity.Services.CloudSave;
using TMPro;
using Unity.Services.Core;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SaveData(Dictionary<string, object> gameData)
    {
        await CloudSaveService.Instance.Data.ForceSaveAsync(gameData);
    }

    public async void LoadData()
    {
        //Dictionary<string, string> serverData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "firstData" });
        
        //if(serverData.ContainsKey("firstData"))
        //{
        //    inpf.text = serverData["firstData"];
        //}
        //else
        //{
        //    print("Key not found");
        //}
    }

    public async void DeleteKey()
    {
        await CloudSaveService.Instance.Data.ForceDeleteAsync("firstData");
    }

    public async void RetriveAllKeys()
    {
        List<string> allKeys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();

        for (int i = 0; i < allKeys.Count; i++)
        {
            print(allKeys[i]);
        }
    }
}
