using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.EventSystems;
using TMPro;

public class BPosition : MonoBehaviour
{

    public ButtonUI buttonUIScript;
    public Installable installableScript;
    public Outline outline;
    public MeshRenderer meshRenderer;

    Sprite APositionIcon;
    Sprite BPositionIcon;

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
            if (meshRenderer.enabled)
            {
                ProfileButtonSetting();
            }
        }
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //UI position to mouse position when clicked
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            buttonUIScript.transform.position = new Vector3(x, y, 0);

            //Clearing UI button when clicked so it's doesn't stack
            buttonUIScript.ClearButton();

            //Setting UI button for this profile
            buttonUIScript.SetName(this.name + " " + "(Removed)");

            GameObject AButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            GameObject BButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "APositionIcon")
                {
                    APositionIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
                if (buttonUIScript.buttonIconManager[i].iconName == "BPositionIcon")
                {
                    BPositionIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            buttonUIScript.AddButton(AButton, APositionIcon, "Install", installableScript.ButtonInstall);
            buttonUIScript.AddButton(BButton, BPositionIcon, "Remove", installableScript.ButtonRemove);
        }
    }

}
