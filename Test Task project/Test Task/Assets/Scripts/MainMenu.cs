using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private string jsonString;
    private JObject jObject;
    private JToken enemyToken, playerToken;
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void ReloadStatistics()
    {
        jsonString = File.ReadAllText("log.json");
        jObject = JsonConvert.DeserializeObject(jsonString) as JObject;
        enemyToken = jObject.SelectToken("Enemy");
        playerToken = jObject.SelectToken("Player");
        enemyToken.Replace(0);
        playerToken.Replace(0);
        string updatedJsonString = jObject.ToString();
        File.WriteAllText("log.json", updatedJsonString);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
