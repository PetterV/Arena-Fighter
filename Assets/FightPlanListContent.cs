using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlanListContent : MonoBehaviour {

	public GameObject fightPlanButton;
	public List<MatchPlan> matchPlans;
	public bool ListPopulated = false;

	public void SetUp (List<MatchPlan> matchPlans) {
		if (!ListPopulated) {
			AddMatchplansToList (matchPlans);
		}
	}

	public void AddMatchplansToList (List<MatchPlan> matchPlans) {
		foreach (MatchPlan m in matchPlans) {
			if (m.PlacedInList == false) {
				GameObject newButton = Instantiate (fightPlanButton) as GameObject;
				newButton.transform.SetParent (fightPlanButton.transform.parent);
				newButton.transform.localScale = fightPlanButton.transform.localScale;
				FightPlanListButton newButtonInfo = newButton.GetComponent<FightPlanListButton> ();
				newButtonInfo.matchPlan = m;
				newButtonInfo.fighter1 = m.Challenger;
				newButtonInfo.fighter2 = m.Defender;
				newButton.SetActive (true);
				newButtonInfo.UpdateFightPlanButtonInfo ();
				m.PlacedInList = true;
				m.FightPlanListButton = newButton;
			}
		}
		ListPopulated = true;
	}

	public void ClearList(){
		//TODO: Add functions here to destroy all relevant buttons
		ListPopulated = false;
	}
}
