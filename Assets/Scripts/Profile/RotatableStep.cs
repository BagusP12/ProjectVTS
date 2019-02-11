using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[AddComponentMenu("Profiles/RotatableStep")]
[RequireComponent(typeof(Entity))]
public class RotatableStep : MonoBehaviour
{

    ButtonUI buttonUIScript;

    public float duration = 1f;
    public int rotatedStepStartStateIndex;
    public RotateAngleData[] rotateAngleData;

    Sprite buttonRotateStepIcon;

    int indexRotation;
    bool canRotate = true;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();

        for (int i = 0; i < rotateAngleData.Length; i++)
        {
            if (i == rotatedStepStartStateIndex)
            {
                indexRotation = rotatedStepStartStateIndex;
                transform.rotation = Quaternion.Euler(rotateAngleData[i].rotateAngle);
            }
        }
    }

    public void ButtonRotateStepObject(int index)
    {
        if (indexRotation != index)
        {
            if (canRotate)
            {
                canRotate = false;
                for (int i = 0; i < rotateAngleData.Length; i++)
                {
                    if (i == index)
                    {
                        StartCoroutine(RotateStepOverSeconds(gameObject, rotateAngleData[i].rotateAngle, duration));
                        buttonUIScript.AddUserAction("Rotate " + rotateAngleData[i].rotateName + " " + gameObject.name);
                        indexRotation = index;
                    }
                }
            }
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator RotateStepOverSeconds(GameObject objectToMove, Vector3 direction, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingRot = objectToMove.transform.eulerAngles;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.rotation = Quaternion.Slerp(Quaternion.Euler(startingRot), Quaternion.Euler(direction), elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.rotation = Quaternion.Euler(direction);

        canRotate = true;
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "RotateStepIcon")
                {
                    buttonRotateStepIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            for (int i = 0; i < rotateAngleData.Length; i++)
            {
                GameObject rotateStepButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

                buttonUIScript.AddButtonArgsInteger(rotateStepButton, buttonRotateStepIcon, rotateAngleData[i].rotateName, ButtonRotateStepObject, i);
            }
        }
    }

}

[Serializable]
public class RotateAngleData
{
    public string rotateName;
    public Vector3 rotateAngle;
}

