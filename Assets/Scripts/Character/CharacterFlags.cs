using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlags {
    public GameController gameController;
    public Dictionary<string, int> flagList = new Dictionary<string, int>();

    public CharacterFlags()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void AddFlag(string flag, int duration = 0)
    {
        int endDate;
        int flagValue;

        //Calculate the duration if the flag is applied
        if (duration != 0)
        {
            endDate = gameController.DaysProgressed + duration;
        }
        else
        {
            endDate = 0;
        }

        //Compare to the existing flag if one exists
        if (flagList.TryGetValue(flag, out flagValue))
        {
            //If the flag already exists indefinitely, don't set one
            if (flagValue == 0)
            {
                //Do nothing about it
            }
            else
            {
                //Set a new end date if the one supplied is greater or indefinite
                if (flagValue > endDate || endDate == 0 && flagValue != 0)
                {
                    flagValue = endDate;
                }
            }
        }
        else
        {
            flagList.Add(flag, endDate);
        }
        
    }

    //Method for clearing a specific flag from a character
        //Will NOT produce an error even if the character does not have the flag in question
    public void RemoveFlag(string flag)
    {
        if (flagList.ContainsKey(flag))
        {
            flagList.Remove(flag);
            Debug.Log("Removed the flag " + flag);
        }
        else
        {
            Debug.Log("Tried to remove the flag " + flag + ", but could not find it.");
        }
    }

    //Run this method on daily ticks for all characters
    public void CheckFlagDurations()
    {
        foreach(KeyValuePair<string, int> e in flagList)
        {
            if(e.Value != 0 && e.Value > gameController.DaysProgressed)
            {
                RemoveFlag(e.Key);
            }
        }
    }
      
    //Method to check whether a character has a specific flag
    public bool HasFlag(string flagToCheck)
    {
        bool hasFlag = false;
        if (flagList.ContainsKey(flagToCheck))
        {
            hasFlag = true;
        }

        return hasFlag;
    }
}
