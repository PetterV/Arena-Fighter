using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour {

	public FightPanelFighter fighterBox1;
	public FightPanelFighter fighterBox2;
	public Fight1v1 Fight;
	public Text textLog;
	//float timeWaitedForTick;
	//Determines the speed of fights
	//public float waitPerTick = 1.0f;
	InputController inputController;
	GameController gameController;

	public void SetUpFight(Character fighter1, Character fighter2, Fight1v1 fight){
		Fight = fight;
		fighterBox1.fighter = fighter1;
		fighterBox1.UpdatePanelInfo ();
		fighterBox2.fighter = fighter2;
		fighterBox2.UpdatePanelInfo ();
		fight.fightPanel = this;
	}

	void OnEnable(){
		inputController = GameObject.Find ("InputController").GetComponent<InputController> ();
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		gameController.PauseTicks ();
		inputController.inFight = true;
		textLog.text = "";
	}

	void OnDisable(){
		fighterBox1.SetPanelToDefaultColour ();
		fighterBox2.SetPanelToDefaultColour ();
		inputController.inFight = false;
		gameController.UnPauseTicks ();
		if (Fight.FightOver == false) {
			Fight.StopWatchingFight ();
		}
	}

	// Use this for initialization
	void Start () {
		inputController = GameObject.Find ("InputController").GetComponent<InputController> ();
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Fight.FightOver == false) {
            fighterBox1.fighter.CurrentCooldown -= Time.deltaTime;
            if(fighterBox1.fighter.CurrentCooldown <= 0)
            {
                Fight.Strike(fighterBox1.fighter, fighterBox2.fighter);
                fighterBox1.fighter.CurrentCooldown = fighterBox1.fighter.FullCooldown;
            }

            fighterBox2.fighter.CurrentCooldown -= Time.deltaTime;
            if (fighterBox2.fighter.CurrentCooldown <= 0)
            {
                Fight.Strike(fighterBox2.fighter, fighterBox1.fighter);
                fighterBox2.fighter.CurrentCooldown = fighterBox2.fighter.FullCooldown;
            }


            /*timeWaitedForTick += Time.deltaTime;
			if (timeWaitedForTick > waitPerTick && !Fight.inputController.pauseFight) {
				Fight.FightRoundTick ();
				timeWaitedForTick = 0;
			}*/
		}
	}


	public void UpdateTextLog(string newText){
		textLog.text = textLog.text + "\n" + newText;
	}
}
