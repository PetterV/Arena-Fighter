using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSelectionBox : MonoBehaviour {

	public MatchPlan match;
	public GameObject Defender;
	public Fighter defender;

	void OnEnable(){
		defender = match.Defender;
		FightPanelFighter DefenderInfo = Defender.GetComponent<FightPanelFighter> ();
		DefenderInfo.fighter = defender;
		Defender.SetActive (true);
		DefenderInfo.UpdatePanelInfo ();
	}

	void OnDisable(){
		//GameObject.Find ("FighterSelectionListContent").GetComponent<FighterListContent> ().ClearList ();
	}
}
