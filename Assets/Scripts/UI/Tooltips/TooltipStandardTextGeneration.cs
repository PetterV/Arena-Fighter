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
            string localisedKey = locManager.GetLocalisedValue(k);
            localisedKey = TextCommandHandler.CheckForTextCommands(localisedKey);
            finalTTMessages.Add(localisedKey);
        }
        gameObject.GetComponent<TooltipRegistration>().ttMessages = finalTTMessages;
    }
}
