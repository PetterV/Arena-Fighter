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

    void Awake() {
        Tooltip = GameObject.Find("TooltipBase");
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (Tooltip == null)
        {
            Tooltip = GameObject.Find("TooltipBase");
        }
        Tooltip.GetComponent<TooltipHandling>().Open(ttMessages);
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        Tooltip.GetComponent<TooltipHandling>().Close();
    }

    public void CloseTooltip()
    {
        Tooltip.GetComponent<TooltipHandling>().Close();
    }
}

