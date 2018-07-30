using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalisedText : MonoBehaviour {

	public string key;

	void Start (){
		Text text = GetComponent<Text> ();
		text.text = LocalisationManager.instance.GetLocalisedValue (key);
	}
}
