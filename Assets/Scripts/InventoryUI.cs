using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{

    public RectTransform inventoryRectTransform;
    public GameObject toolPrefab;

    public GameObject activeTool;

    List<GameObject> inventoryToolList = new List<GameObject>();

    public void AddInventory(GameObject tool, Sprite icon, string toolName, UnityEngine.Events.UnityAction action)
    {
        tool.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        tool.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = toolName;

        tool.GetComponent<Button>().onClick.AddListener(() => { action(); });
        tool.transform.SetParent(inventoryRectTransform.transform);
        inventoryToolList.Add(tool);
    }

}
