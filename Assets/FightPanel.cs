using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPanel : MonoBehaviour {

	public FightPanelFighter fighterBox1;
	public FightPanelFighter fighterBox2;
	public Fight1v1 Fight;
	float timeWaitedForTick;
	float waitPerTick = 0.05f;

	public void SetUpFight(Fighter fighter1, Fighter fighter2, Fight1v1 fight){
		Fight = fight;
		fighterBox1.fighter = fighter1;
		fighterBox1.UpdatePanelInfo ();
		fighterBox2.fighter = fighter2;
		fighterBox2.UpdatePanelInfo ();
	}

	void OnEnable(){
		GameObject.Find ("GameController").GetComponent<GameController> ().PauseTicks ();
		GameObject.Find ("InputController").GetComponent<InputController> ().inFight = true;
	}

	void OnDisable(){
		GameObject.Find ("InputController").GetComponent<InputController> ().inFight = false;
		GameObject.Find ("GameController").GetComponent<GameController> ().UnPauseTicks ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Fight.FightOver == false) {
			timeWaitedForTick += Time.deltaTime;
			if (timeWaitedForTick > waitPerTick) {
				Fight.FightRoundTick ();
				timeWaitedForTick = 0;
			}
		}
	}
}
