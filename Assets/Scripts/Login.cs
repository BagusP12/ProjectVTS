using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public Connection connectionScript;

    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI loadingText;

    public GameObject loadingSlider;

    private string username;
    private string password;
    private string message;

    private void Start()
    {
        usernameInputField.ActivateInputField();
    }

    public void LoginButton()
    {
        Debug.Log("Login");
        username = usernameInputField.text;
        password = passwordInputField.text;
        connectionScript.Login(username, password);
        StartCoroutine(LoadAsynchronously(1));
    }

    public void ExitButton()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingSlider.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingText.text = "Loading " + (progress * 100).ToString("0") + "%";

            loadingSlider.GetComponent<Slider>().value = progress;
            yield return null;
        }
    }

}
