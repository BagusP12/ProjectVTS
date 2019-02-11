using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUI : MonoBehaviour
{

    public UserInterfaceManager userInterfaceManager;

    public RectTransform rectTransform;
    public GameObject namePanelPrefab;

    public ButtonIcon[] buttonIconManager;

    List<String> userActions = new List<String>();
    List<GameObject> buttonList = new List<GameObject>();

    GameObject namePanel;

    //Width and height each GameObject in ButtonUI
    float UIWidth = 200f;
    float UIHeight = 30f;
    float UIHeightMultiplier = 30f;
    float UIDefaultWidth;
    float UIDefaultHeight;

    //User Action counter
    int actionIndex = 0;

    private void Start()
    {
        UIDefaultWidth = rectTransform.sizeDelta.x;
        UIDefaultHeight = rectTransform.sizeDelta.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClearButton();
        }
    }

    public void SetName(string objName)
    {
        namePanel = Instantiate(namePanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        namePanel.transform.SetParent(this.transform);
        namePanel.transform.Find("ObjectNameText").GetComponent<TextMeshProUGUI>().text = objName;
    }

    public void AddButton(GameObject button, Sprite icon, string buttonName, UnityEngine.Events.UnityAction action)
    {
        button.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        button.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = buttonName;

        button.GetComponent<Button>().onClick.AddListener(() => { action(); });
        button.transform.SetParent(this.transform);
        buttonList.Add(button);

        SetUIHeight();
    }

    public void AddButtonArgsInteger(GameObject button, Sprite icon, string buttonName, UnityEngine.Events.UnityAction<int> action, int args)
    {
        button.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        button.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = buttonName;

        button.GetComponent<Button>().onClick.AddListener(() => { action(args); });
        button.transform.SetParent(this.transform);
        buttonList.Add(button);

        SetUIHeight();
    }

    public void AddButtonArgsFloat(GameObject button, Sprite icon, string buttonName, UnityEngine.Events.UnityAction<float> action, float args)
    {
        button.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        button.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = buttonName;

        button.GetComponent<Button>().onClick.AddListener(() => { action(args); });
        button.transform.SetParent(this.transform);
        buttonList.Add(button);

        SetUIHeight();
    }

    public void AddButtonArgsString(GameObject button, Sprite icon, string buttonName, UnityEngine.Events.UnityAction<string> action, string args)
    {
        button.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        button.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = buttonName;

        button.GetComponent<Button>().onClick.AddListener(() => { action(args); });
        button.transform.SetParent(this.transform);
        buttonList.Add(button);

        SetUIHeight();
    }

    public void SetUIHeight()
    {
        //Adding UI height each time new button added
        UIHeight = (buttonList.Count + 1) * UIHeightMultiplier;
        rectTransform.sizeDelta = new Vector2(UIWidth, UIHeight);
    }

    public void ClearButton()
    {
        Destroy(namePanel);

        if (buttonList.Count > 0)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                Destroy(buttonList[i]);
            }
            //Reset UI height each time button cleared
            rectTransform.sizeDelta = new Vector2(UIDefaultWidth, UIDefaultHeight);

            buttonList.Clear();
            Debug.Log("Clear Button Menu");
        }
    }

    public void AddUserAction(string action)
    {
        actionIndex += 1;
        string UserAction = "<color=#FFAE00>User Action " + "#" + actionIndex + " : </color>" + action;
        userActions.Add(UserAction);
        userInterfaceManager.messageText.text = UserAction;
    }

}

[Serializable]
public class ButtonIcon
{
    public String iconName;
    public Sprite iconSprite;
}
