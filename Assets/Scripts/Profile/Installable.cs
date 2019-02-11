using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEngine.EventSystems;

[AddComponentMenu("Profiles/Installable")]
public class Installable : MonoBehaviour
{

    ButtonUI buttonUIScript;
    public float fadeDuration = 1f; 
    public GameObject objectA;
    public GameObject objectB;
    public bool installState = false;

    MeshRenderer meshRendererA;
    MeshRenderer meshRendererB;

    Material materialA;
    Material materialB;

    Color materialAColor;
    Color materialBColor;

    bool canSwap = true;

    private void Start()
    {
        buttonUIScript = FindObjectOfType<ButtonUI>();

        //ObjectA
        objectA.AddComponent<APosition>();
        objectA.AddComponent<Outline>();
        meshRendererA = objectA.GetComponent<MeshRenderer>();
        materialA = meshRendererA.material;
        materialAColor = materialA.color;

        objectA.GetComponent<APosition>().buttonUIScript = buttonUIScript;
        objectA.GetComponent<APosition>().installableScript = this;
        objectA.GetComponent<APosition>().outline = objectA.GetComponent<Outline>();
        objectA.GetComponent<APosition>().meshRenderer = meshRendererA;

        objectA.GetComponent<Outline>().enabled = false;

        //ObjectB
        objectB.AddComponent<BPosition>();
        objectB.AddComponent<Outline>();
        meshRendererB = objectB.GetComponent<MeshRenderer>();
        materialB = meshRendererB.material;
        materialBColor = materialB.color;

        objectB.GetComponent<BPosition>().buttonUIScript = buttonUIScript;
        objectB.GetComponent<BPosition>().installableScript = this;
        objectB.GetComponent<BPosition>().outline = objectB.GetComponent<Outline>();
        objectB.GetComponent<BPosition>().meshRenderer = meshRendererB;

        objectB.GetComponent<Outline>().enabled = false;

        if (installState)
        {
            meshRendererA.enabled = true;
            meshRendererB.enabled = false;

            materialBColor.a = 0f;
            materialB.color = materialBColor;
        }
        else
        {
            meshRendererA.enabled = false;
            meshRendererB.enabled = true;

            materialAColor.a = 0f;
            materialA.color = materialAColor;
        }
    }

    public void ButtonInstall()
    {
        if (!installState)
        {
            if (canSwap)
            {
                canSwap = false;
                StartCoroutine(InstallFade(fadeDuration));
                Debug.Log("Install");
            }
        }
        buttonUIScript.ClearButton();
    }

    public void ButtonRemove()
    {
        if (installState)
        {
            if (canSwap)
            {
                canSwap = false;
                StartCoroutine(RemoveFade(fadeDuration));
                Debug.Log("Remove");
            }
        }
        buttonUIScript.ClearButton();
    }

    IEnumerator InstallFade(float duration)
    {
        meshRendererA.enabled = true;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            //Do Something
            materialAColor.a = Mathf.Clamp01(elapsedTime / duration);
            materialA.color = materialAColor;

            materialBColor.a = 1.0f - Mathf.Clamp01(elapsedTime / duration);
            materialB.color = materialBColor;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //materialAColor.a = 255f;
        //materialA.color = materialAColor;
        //materialBColor.a = 0f;
        //materialBColor = materialBColor;

        meshRendererB.enabled = false;

        installState = !installState;

        objectB.GetComponent<Outline>().enabled = false;
        canSwap = true;
    }

    IEnumerator RemoveFade(float duration)
    {
        meshRendererB.enabled = true;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            //Do Something
            materialAColor.a = 1.0f - Mathf.Clamp01(elapsedTime / duration);
            materialA.color = materialAColor;

            materialBColor.a = Mathf.Clamp01(elapsedTime / duration);
            materialB.color = materialBColor;

            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        //materialAColor.a = 0f;
        //materialA.color = materialAColor;
        //materialBColor.a = 255f;
        //materialBColor = materialBColor;

        meshRendererA.enabled = false;

        installState = !installState;

        objectA.GetComponent<Outline>().enabled = false;
        canSwap = true;
    }

}
