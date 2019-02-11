using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Profiles/PressableButton")]
[RequireComponent(typeof(Entity))]
public class PressableButton : MonoBehaviour
{
    ButtonUI buttonUIScript;

    public Vector3 pressedPosition = new Vector3(0f, 0f, 1f);

    Sprite pressedIcon;

    bool canMove = true;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();
    }

    public void PressedButton()
    {
        if (canMove)
        {
            canMove = false;
            StartCoroutine(PressedOverSeconds(gameObject, pressedPosition, 0.1f));
            buttonUIScript.AddUserAction("Button Pressed " + gameObject.name);
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator PressedOverSeconds(GameObject objectToMove, Vector3 pressedDirection, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < (seconds / 2) && elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, startingPos + pressedDirection, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (elapsedTime > (seconds / 2) && elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos + pressedDirection, startingPos, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = startingPos;

        canMove = true;
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "PressedIcon")
                {
                    pressedIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            GameObject pressedButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(pressedButton, pressedIcon, "Press Button", PressedButton);
        }
    }

}

