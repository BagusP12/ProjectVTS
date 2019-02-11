using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Profiles/Moveable")]
[RequireComponent(typeof(Entity))]
public class Moveable : MonoBehaviour
{

    ButtonUI buttonUIScript;

    public float duration = 1f;
    public bool hideAfterMove = false;
    public bool removedState = false;

    public Vector3 moveDirection = new Vector3(0f, 5f, 0f);

    MeshRenderer meshRenderer;

    Sprite buttonMoveIcon;
    Sprite buttonBackIcon;

    bool canMove = true; 

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();

        meshRenderer = GetComponent<MeshRenderer>();

        if (removedState)
        {
            transform.position = transform.position + moveDirection;
        }
    }

    public void ButtonMoveObject()
    {
        if (!removedState)
        {
            if (canMove)
            {
                canMove = false;
                StartCoroutine(MoveOverSeconds(gameObject, moveDirection, duration));
                buttonUIScript.AddUserAction("Move " + gameObject.name);
                removedState = !removedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    public void ButtonBackObject()
    {
        if (removedState)
        {
            if (canMove)
            {
                canMove = false;
                StartCoroutine(MoveOverSeconds(gameObject, -moveDirection, duration));
                buttonUIScript.AddUserAction("Move Back " + gameObject.name);
                removedState = !removedState;
            }
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 direction, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, startingPos + direction, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = startingPos + direction;

        canMove = true;

        if (hideAfterMove)
        {
            meshRenderer.enabled = false;
        }
    }

    public void ProfileButtonSetting()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            for (int i = 0; i < buttonUIScript.buttonIconManager.Length; i++)
            {
                if (buttonUIScript.buttonIconManager[i].iconName == "MoveIcon")
                {
                    buttonMoveIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
                if (buttonUIScript.buttonIconManager[i].iconName == "BackIcon")
                {
                    buttonBackIcon = buttonUIScript.buttonIconManager[i].iconSprite;
                }
            }

            GameObject moveButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            GameObject backButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(moveButton, buttonMoveIcon, "Move", ButtonMoveObject);
            buttonUIScript.AddButton(backButton, buttonBackIcon, "Back", ButtonBackObject);
        }
    }

}
