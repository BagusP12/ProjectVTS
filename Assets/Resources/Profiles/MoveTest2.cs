using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.EventSystems;
using TMPro;

public class MoveTest2 : MonoBehaviour
{

    public ButtonUI buttonUIScript;

    public float duration = 1f;
    public bool hideAfterMove = false;
    public bool removedState = false;

    public Vector3 moveDirection = new Vector3(0f, 10f, 0f);

    GameObject moveButton;
    GameObject backButton;

    Outline outline;
    MeshRenderer meshRenderer;

    [Space(15)]
    public Sprite buttonMoveIcon;
    public Sprite buttonBackIcon;

    bool canMove = true;

    private void Start()
    {
        gameObject.AddComponent<Outline>();
        outline = GetComponent<Outline>();
        meshRenderer = GetComponent<MeshRenderer>();

        outline.enabled = false;

        if (removedState)
        {
            transform.position = transform.position + moveDirection;
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
        ProfileButtonSetting();
    }

    public void ButtonMoveObject()
    {
        if (!removedState)
        {
            if (canMove)
            {
                canMove = false;
                StartCoroutine(MoveOverSeconds(gameObject, moveDirection, duration));
                removedState = !removedState;
                Debug.Log(gameObject.name + " " + "Move From : " + transform.position + ", " + "To : " + moveDirection + ", " + "Removed State : " + removedState);
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
                removedState = !removedState;
                Debug.Log(gameObject.name + " " + "Move From : " + transform.position + ", " + "To : " + -moveDirection + ", " + "Removed State : " + removedState);
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
            //UI position to mouse position when clicked
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            buttonUIScript.transform.position = new Vector3(x, y, 0);

            Debug.Log(gameObject.name + " " + "Clicked, Button Added");

            //Clearing UI button when clicked so it's doesn't stack
            buttonUIScript.ClearButton();

            //Setting UI button for this profile
            buttonUIScript.SetName(this.name);

            moveButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);
            backButton = Instantiate(Resources.Load("ButtonTemplate") as GameObject);

            buttonUIScript.AddButton(moveButton, buttonMoveIcon, "Move", ButtonMoveObject);
            buttonUIScript.AddButton(backButton, buttonBackIcon, "Back", ButtonBackObject);
        }
    }

}
