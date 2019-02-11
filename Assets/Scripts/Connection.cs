using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Text;


public class Connection : MonoBehaviour
{

    [Header("Connection Settings")]

    public string configFile;
    public string serverUrl;
    public string smartfoxUrl;
    public string smartfoxPort;

    private List<string> lineText;

    private void Awake()
    {
#if UNITY_EDITOR
        configFile = Application.dataPath + "/StreamingAssets/Config.txt";
#else
		configFile = Application.dataPath+"/StreamingAssets/Config.txt";
#endif

        lineText = new List<string>();
        Load(configFile);
    }

    private void Start()
    {
        serverUrl = lineText[1].Substring(7);
        smartfoxUrl = lineText[2].Substring(16);
        smartfoxPort = lineText[3].Substring(14);

        PlayerPrefs.SetString("SERVER_URL", serverUrl);
        PlayerPrefs.SetString("SMARTFOX_URL", smartfoxUrl);
        PlayerPrefs.SetString("SMARTFOX_PORT", smartfoxPort);

        Debug.Log("Server URL : " + serverUrl + ", " + "Smartfox URL : " + smartfoxUrl + ", " + "Smartfox Port : " + smartfoxPort);
    }

    private void Load(string fileName)
    {
        try
        {
            string line;
            StreamReader reader = new StreamReader(fileName, Encoding.Default);
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        lineText.Add(line.ToString());
                    }
                } while (line != null);
                reader.Close();
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void Login(string username, string password)
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("username", username);
        wwwForm.AddField("Password", password);

        string apiURL = serverUrl + "api/login/";
        WWW www = new WWW(apiURL, wwwForm);
        StartCoroutine(CallDataLogin(www));
    }

    IEnumerator CallDataLogin(WWW www)
    {
        yield return www;
        if (www.error == null)
        {

        }
        else
        {
            
        }
    }

    string JsonRead(JsonData jsonData, string defaultValue)
    {
        string result;
        if (jsonData != null)
        {
            result = jsonData.ToString();
        }
        else
        {
            result = defaultValue;
        }
        return result;
    }

}
