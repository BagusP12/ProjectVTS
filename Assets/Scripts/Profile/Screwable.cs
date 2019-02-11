using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Profiles/Screwable")]
[RequireComponent(typeof(Entity))]
public class Screwable : MonoBehaviour
{

    ButtonUI buttonUIScript;

    public float duration = 1f;
    public bool screwedState = false;
    public float rotationRevolution = 2;
    public Vector3 screwMoveDirection = new Vector3(0f, 5f, 0f);

    Sprite buttonScreIcon;
    Sprite buttonUnscrewIcon;

    //float angle = 360.0f;
    //float time = 1.0f;
    bool canScrew = true;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();

        if (screwedState)
        {
            transform.position = transform.position + screwMoveDirection;
        }
    }

    public void ButtonScrewObject()
    {
        if (!screwedState)
        {
            if (canScrew)
            {
                canScrew = false;
                StartCoroutine(ScrewOverSeconds(gameObject, screwMoveDirection, duration));
                buttonUIScript.AddUserAction("Screw " + gameObject.name);
                screwedState = !screwedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    public void ButtonUnscrewObject()
    {
        if (screwedState)
        {
            if (canScrew)
            {
                canScrew = false;
                StartCoroutine(ScrewOverSeconds(gameObject, -screwMoveDirection, duration));
                buttonUIScript.AddUserAction("Unscrew " + gameObject.name);
                screwedState = !screwedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator ScrewOverSeconds(GameObject objectToMove, Vector3 direction, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        Vector3 startingRot = objectToMove.transform.eulerAngles;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, startingPos + direction, (elapsedTime / seconds));
            //objectToMove.transform.RotateAround(Vector3.zero, -direction, (angle * rotationRevolution) * Time.deltaTime / time);
            objectToMove.transform.Rotate(-direction * rotationRevolution);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = startingPos + direction;

        canScrew = true;
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "ScrewIcon")
                {
                    buttonScreIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
                if (buttonUIScript.buttonIconManager[i].iconName == "UnscrewIcon")
                {
                    buttonUnscrewIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            GameObject screwButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            GameObject unscrewButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(screwButton, buttonScreIcon, "Screw", ButtonScrewObject);
            buttonUIScript.AddButton(unscrewButton, buttonUnscrewIcon, "Unscrew", ButtonUnscrewObject);
        }
    }

}
