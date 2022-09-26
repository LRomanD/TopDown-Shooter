using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class UIScore : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;

    [SerializeField] Text playerScore;
    [SerializeField] Text enemyScore;

    [SerializeField] string jsonString;
    [SerializeField] JObject jObject;
    [SerializeField] JToken enemyToken, playerToken;

    void Update()
    {
        jsonString = File.ReadAllText("log.json");
        jObject = JsonConvert.DeserializeObject(jsonString) as JObject;
        enemyToken = jObject.SelectToken("Enemy");
        playerToken = jObject.SelectToken("Player");
        enemyScore.text = enemyToken.ToString();
        playerScore.text = playerToken.ToString();
    }
    public void RewriteData(int enemy, int player)
    {
        jsonString = File.ReadAllText("log.json");
        jObject = JsonConvert.DeserializeObject(jsonString) as JObject;
        enemyToken = jObject.SelectToken("Enemy");
        playerToken = jObject.SelectToken("Player");
        enemyToken.Replace(Convert.ToInt32(enemyToken) + enemy);
        playerToken.Replace(Convert.ToInt32(playerToken) + player);
        string updatedJsonString = jObject.ToString();
        File.WriteAllText("log.json", updatedJsonString);
    }
}
