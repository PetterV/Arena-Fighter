using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterListContent : MonoBehaviour {

	public GameObject fighterButton;
	public List<Fighter> fighters;
	public bool ListPopulated = false;

	public void SetUp (List<Fighter> fighters) {
		if (!ListPopulated) {
			PopulateList (fighters);
		}
	}

	void PopulateList (List<Fighter> fighters) {
		foreach (Fighter f in fighters) {
			GameObject newButton = Instantiate (fighterButton) as GameObject;
			newButton.transform.SetParent (fighterButton.transform.parent);
			newButton.transform.localScale = fighterButton.transform.localScale;
			FighterListButton newButtonInfo = newButton.GetComponent<FighterListButton> ();
			newButtonInfo.fighter = f;
			f.FighterListButton = newButtonInfo;
			newButton.SetActive(true);
			newButtonInfo.UpdateFighterButtonInfo();
		}
		ListPopulated = true;
	}

	public void ClearList(){
		//TODO: Add functions here to destroy all relevant buttons
		ListPopulated = false;
	}
}
