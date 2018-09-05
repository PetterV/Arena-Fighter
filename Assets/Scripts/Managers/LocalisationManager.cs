using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalisationManager : MonoBehaviour {

	public static LocalisationManager instance;

	private Dictionary<string, string> localisedText;
	private bool isReady = false;
	private string missingTextString = "No text found for this key in the current language!";

	void Awake ()
	{
		if (instance == null) {
			instance = this;
            LoadAllFiles();
            isReady = true;
        } else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}


    void LoadAllFiles()
    {
        LoadLocalisedText("Localisation Files/localisation_en.json");
        LoadLocalisedText("Localisation Files/tooltip_localisation_en.json");
    }

	public void LoadLocalisedText(string fileName)
	{
        //TODO: Create new dictionary if language has changed... (Separate method to handle language change?)
        if (localisedText == null)
        {
            localisedText = new Dictionary<string, string>();
        }

        string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			LocalisationData loadedData = JsonUtility.FromJson<LocalisationData> (dataAsJson);

			for (int i = 0; i < loadedData.items.Length; i++) {
				localisedText.Add (loadedData.items[i].key, loadedData.items[i].value);
			}
            
			Debug.Log ("Loaded localisation. Database contains: " + localisedText.Count + " entries.");
		} else {
			Debug.LogError ("Cannot find localisation file for language!");
		}
	}

	public string GetLocalisedValue(string key){
		string result = missingTextString;
		if (localisedText.ContainsKey (key)) {
			result = localisedText [key];
		}

		return result;
	}

	public bool GetIsReady(){
		return isReady;
	}
}
