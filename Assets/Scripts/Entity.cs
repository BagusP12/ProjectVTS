using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;

public class Entity : MonoBehaviour
{
    ButtonUI buttonUIScript;

    List<MonoBehaviour> profiles = new List<MonoBehaviour>();

    Outline outline;
    MeshRenderer meshRenderer;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();

        gameObject.AddComponent<Outline>();
        outline = GetComponent<Outline>();
        outline.enabled = false;

        meshRenderer = GetComponent<MeshRenderer>();


        //Adding Profile
        if (gameObject.GetComponent<Moveable>() != null)
        {
            profiles.Add(this.GetComponent<Moveable>());
        }
        if (gameObject.GetComponent<PressableButton>() != null)
        {
            profiles.Add(this.GetComponent<PressableButton>());
        }
        if (gameObject.GetComponent<Rotatable>() != null)
        {
            profiles.Add(this.GetComponent<Rotatable>());
        }
        if (gameObject.GetComponent<RotatableStep>() != null)
        {
            profiles.Add(this.GetComponent<RotatableStep>());
        }
        if (gameObject.GetComponent<Screwable>() != null)
        {
            profiles.Add(this.GetComponent<Screwable>());
        }
        if (gameObject.GetComponent<InventoryTool>() != null)
        {
            profiles.Add(this.GetComponent<InventoryTool>());
        }
    }

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (meshRenderer.enabled)
            {
                outline.enabled = true;
            }
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ButtonSetting();

            //Executing Button Profile
            for (int i = 0; i < profiles.Count; i++)
            {
                if (profiles[i] is Moveable)
                {
                    profiles[i].GetComponent<Moveable>().ProfileButtonSetting();
                }
                if (profiles[i] is PressableButton)
                {
                    profiles[i].GetComponent<PressableButton>().ProfileButtonSetting();
                }
                if (profiles[i] is Rotatable)
                {
                    profiles[i].GetComponent<Rotatable>().ProfileButtonSetting();
                }
                if (profiles[i] is RotatableStep)
                {
                    profiles[i].GetComponent<RotatableStep>().ProfileButtonSetting();
                }
                if (profiles[i] is Screwable)
                {
                    profiles[i].GetComponent<Screwable>().ProfileButtonSetting();
                }
                if (profiles[i] is InventoryTool)
                {
                    profiles[i].GetComponent<InventoryTool>().ProfileButtonSetting();
                }
            }
        }
    }

    private void ButtonSetting()
    {
        //UI position to mouse position when clicked
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        buttonUIScript.transform.position = new Vector3(x, y, 0);

        //Clearing UI button when clicked so it's doesn't stack
        buttonUIScript.ClearButton();

        //Setting UI button for this profile
        buttonUIScript.SetName(this.name);
    }

    //private static Dictionary<string, MonoScript> AllScripts = new Dictionary<string, MonoScript>();
    //public static void ListScriptObjects()
    //{
    //    AllScripts.Clear();
    //    UnityEngine.Object[] scripts = Resources.LoadAll("Profiles");
    //    foreach (UnityEngine.Object script in scripts)
    //    {
    //        if (script.GetType().Equals(typeof(MonoScript)))
    //        {
    //            AllScripts.Add(script.name, (MonoScript)script);
    //            Debug.Log(script.name);
    //        }
    //    }
    //    Debug.Log("Profile Count : " + AllScripts.Count.ToString());
    //}

}
