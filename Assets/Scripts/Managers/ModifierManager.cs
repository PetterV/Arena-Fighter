using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModifierManager : MonoBehaviour {

    public static ModifierManager instance;

    public List<CharacterModifierItem> CharacterModifierTypes = new List<CharacterModifierItem>();
    private bool isReady = false;
    private string fileName = "CharacterModifiers.json";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            BuildCharacterModifierList(fileName);
            isReady = true;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void BuildCharacterModifierList(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            CharacterModifierData loadedData = JsonUtility.FromJson<CharacterModifierData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                //localisedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                CharacterModifierTypes.Add(loadedData.items[i]);
            }

            Debug.Log("Loaded modifiers. Database contains: " + CharacterModifierTypes.Count + " entries.");
        }
        else
        {
            Debug.LogError("Could not find any modifier files!");
        }
    }
}
