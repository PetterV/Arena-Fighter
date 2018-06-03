using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanelFighter : MonoBehaviour {

	public Fighter fighter;

	public Text FighterName;
	public Text Level;
	public Text Race;
	public Text Class;
	public Text health;
	public Text maxHealth;
	public Text energy;
	public Text maxEnergy;

	public void UpdatePanelInfo(){
		FighterName.text = fighter.FirstName + " " + fighter.LastName;
		Level.text = fighter.Level.ToString ();
		Race.text = fighter.Race.RaceName;
		Class.text = fighter.MyClass.Name;
		maxHealth.text = fighter.MaxHealth.ToString();
		maxEnergy.text = fighter.MaxEnergy.ToString();	
	}

	// Update is called once per frame
	void Update () {
		health.text = fighter.CurrentHealth.ToString();
		energy.text = fighter.CurrentEnergy.ToString();
	}
}
