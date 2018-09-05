using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextCommandHandler {
    
	public static string CheckForTextCommands(string originalText, Character character = null, Team team = null)
    {
        if (originalText.Contains("£"))
        {
            string newText;

            int startIndex = originalText.IndexOf("£");
            int endIndex = originalText.IndexOf("#");

            string commandToReplace = originalText.Substring(startIndex, endIndex - startIndex);


            /*Method list: 
                CharacterFirstName
                CharacterLastName
                SheHe
                SheHeCap
             */

            string newString = "No text found for function: " + commandToReplace + " ! ";

            if (commandToReplace.Contains("FirstName"))
            {
                if (character != null)
                {
                    newString = CharacterFirstName(character);
                }
                else
                {
                    Debug.LogError("No character for CharacterFirstName!");
                }
            }
            else if (commandToReplace.Contains("LastName"))
            {
                if(character != null)
                {
                    newString = CharacterLastName(character);
                }
                else
                {
                    Debug.LogError("No character for CharacterLastName!");
                }
            }
            else if (commandToReplace.Contains("SheHe"))
            {
                if (character != null)
                {
                    newString = SheHe(character);
                }
                else
                {
                    Debug.LogError("No character for SheHe!");
                }
            }
            else if (commandToReplace.Contains("SheHeCap"))
            {
                if (character != null)
                {
                    newString = SheHeCap(character);
                }
                else
                {
                    Debug.LogError("No character for SheHeCap!");
                }
            }

            newText = originalText.Replace(commandToReplace, newString);

            /*if (newText.Contains("£"))
            {
                newText = CheckForTextCommands(newText);
            }*/

            return newText;            
        }
        else
        {
            return originalText;
        }
    }
    
    //Individual functions that check for the different possible commands
    private static string CharacterFirstName(Character character)
    {
        string newString = character.FirstName;

        return newString;
    }

    private static string CharacterLastName(Character character)
    {
        string newString = character.LastName;

        return newString;
    }

    private static string SheHe(Character character)
    {
        string newString = character.Gender.SheHe;

        return newString;
    }

    private static string SheHeCap(Character character)
    {
        string newString = character.Gender.SheHeCap;

        return newString;
    }
}
