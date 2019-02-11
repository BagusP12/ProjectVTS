using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[AddComponentMenu("Profiles/InventoryTool")]
[RequireComponent(typeof(Entity))]
public class InventoryTool : MonoBehaviour {

    ButtonUI buttonUIScript;
    InventoryUI inventoryUIScript;

    public string toolName;
    public Sprite toolIcon;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();
        inventoryUIScript = FindObjectOfType<InventoryUI>();

        AddInventory();
    }

    public void SelectToolButton()
    {
        ActivateTool();
        buttonUIScript.ClearButton();
    }

    public void UnselectToolButton()
    {
        DeactivateTool();
        buttonUIScript.ClearButton();
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject selectToolButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            //GameObject unselectToolButton = Instantiate(Ryesources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(selectToolButton, toolIcon, "Select " + toolName, SelectToolButton);
            //buttonUIScript.AddButton(unselectToolButton, toolIcon, "Unselect " + toolName, UnselectToolButton);
        }
    }

    public void AddInventory()
    {
        GameObject selectToolButton = Instantiate(Resources.Load("ToolTemplate") as GameObject);

        inventoryUIScript.AddInventory(selectToolButton, toolIcon, toolName, SelectToolButton);
    }

    public void ActivateTool()
    {
        Destroy(inventoryUIScript.activeTool);

        GameObject tool = Instantiate(Resources.Load("ToolTemplate") as GameObject);
        tool.transform.Find("Icon").GetComponent<Image>().sprite = toolIcon;
        tool.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = toolName;

        tool.GetComponent<Button>().onClick.AddListener(() => { UnselectToolButton(); });
        tool.transform.SetParent(inventoryUIScript.transform.Find("ActiveTool").transform);
        tool.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        inventoryUIScript.activeTool = tool;
        buttonUIScript.AddUserAction("Select " + toolName);
    }

    public void DeactivateTool()
    {
        Destroy(inventoryUIScript.activeTool);
        inventoryUIScript.activeTool = null;
        buttonUIScript.AddUserAction("Unselect " + toolName);
    }

}
