using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipStandardTextGeneration : MonoBehaviour {

    public List<string> keys;
    private List<string> finalTTMessages = new List<string>();
    private LocalisationManager locManager;

    void Start()
    {
        locManager = GameObject.Find("LocalisationManager").GetComponent<LocalisationManager>();
        foreach (string k in keys)
        {
            finalTTMessages.Add(locManager.GetLocalisedValue(k));
            Debug.Log("Added " + locManager.GetLocalisedValue(k));
        }
        gameObject.GetComponent<TooltipRegistration>().ttMessages = finalTTMessages;
    }
}
