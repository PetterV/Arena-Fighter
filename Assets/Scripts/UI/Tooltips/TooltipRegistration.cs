using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipRegistration : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Tooltip;
    public List<string> ttMessages;
    private GameObject textBox;
    private string ttMessage;
    private List<GameObject> newTextBoxes = new List<GameObject>();

    void Awake() {
        Tooltip = GameObject.Find("TooltipBase");
        textBox = Tooltip.GetComponent<TooltipHandling>().textBox;
        textBox.SetActive(false);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Tooltip.GetComponent<TooltipHandling>().Open();
        foreach (string t in ttMessages)
        {   
            GameObject newText = Instantiate(textBox) as GameObject;
            newText.transform.SetParent(textBox.transform.parent);
            newText.transform.localScale = textBox.transform.localScale;
            newText.GetComponent<Text>().text = t;
            newText.SetActive(true);
            newTextBoxes.Add(newText);
        }
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        foreach (GameObject box in newTextBoxes) {
            Destroy(box);
        }
        Tooltip.GetComponent<TooltipHandling>().Close();
    }
}

