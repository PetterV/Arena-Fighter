using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalisationManager : MonoBehaviour {

	public static LocalisationManager instance;

	private Dictionary<string, string> localisedText;
	private bool isReady = false;
	private string missingTextString = "No text found for this key in the current language!";
    private int totalKeys = 0;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			LoadLocalisedText ("localisation_en.json");
        } else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	public void LoadLocalisedText(string fileName)
	{
		localisedText = new Dictionary<string, string> ();
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			LocalisationData loadedData = JsonUtility.FromJson<LocalisationData> (dataAsJson);

			for (int i = 0; i < loadedData.items.Length; i++) {
				localisedText.Add (loadedData.items [totalKeys + i].key, loadedData.items [totalKeys + i].value);
			}

            totalKeys = localisedText.Count - 1;

			Debug.Log ("Loaded localisation. Database contains: " + localisedText.Count + " entries.");
		} else {
			Debug.LogError ("Cannot find localisation file for language!");
		}

		isReady = true;
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
