using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Profiles/Rotatable")]
[RequireComponent(typeof(Entity))]
public class Rotatable : MonoBehaviour
{

    ButtonUI buttonUIScript;

    public float duration = 1f;
    public Vector3 rotateAngle = new Vector3(0f, 0f, -45f);
    public bool rotatedState = false;

    Sprite buttonRotateIcon;
    Sprite reverseRotateIcon;

    bool canRotate = true;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();
        if (rotatedState)
        {
            transform.rotation = Quaternion.Euler(rotateAngle);
        }
    }

    void ButtonRotateObject()
    {
        if (!rotatedState)
        {
            if (canRotate)
            {
                canRotate = false;
                StartCoroutine(RotateOverSeconds(gameObject, rotateAngle, duration));
                buttonUIScript.AddUserAction("Rotate " + gameObject.name);
                rotatedState = !rotatedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    void ButtonReverseRotateObject()
    {
        if (rotatedState)
        {
            if (canRotate)
            {
                canRotate = false;
                StartCoroutine(RotateOverSeconds(gameObject, -rotateAngle, duration));
                buttonUIScript.AddUserAction("Reverse Rotate " + gameObject.name);
                rotatedState = !rotatedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator RotateOverSeconds(GameObject objectToMove, Vector3 direction, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingRot = objectToMove.transform.eulerAngles;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.rotation = Quaternion.Slerp(Quaternion.Euler(startingRot), Quaternion.Euler(startingRot + direction), elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.rotation = Quaternion.Euler(startingRot + direction);

        canRotate = true;
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "RotateIcon")
                {
                    buttonRotateIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
                if (buttonUIScript.buttonIconManager[i].iconName == "ReverseRotateIcon")
                {
                    reverseRotateIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            GameObject rotateButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            GameObject reverseRotateButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(rotateButton, buttonRotateIcon, "Rotate", ButtonRotateObject);
            buttonUIScript.AddButton(reverseRotateButton, reverseRotateIcon, "Reverse Rotate", ButtonReverseRotateObject);
        }
    }

}
