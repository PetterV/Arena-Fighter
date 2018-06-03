using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterDisplayPanel : MonoBehaviour {

	public Fighter fighter;

	public Text FighterName;
	public Text Level;
	public Text Race;
	public Text Class;
	public Text health;
	public Text maxHealth;
	public Text energy;
	public Text maxEnergy;
	public Text currentActivity;
	public Text attack;
	public Text defense;
	public Text magic;
	public Text strategy;
	public Text showcreatureship;
	public Text comebackitude;
	public Text attackAdvance;
	public Text defenseAdvance;
	public Text magicAdvance;
	public Text strategyAdvance;
	public Text showcreatureshipAdvance;
	public Text comebackitudeAdvance;
	public Text activityAttackProgress;
	public Text activityDefenseProgress;
	public Text activityMagicProgress;
	public Text activityStrategyProgress;
	public Text activityShowcreatureshipProgress;
	public Text activityComebackitudeProgress;
	public Text experience;

	public void UpdatePanelInfo(){
		FighterName.text = fighter.FirstName + " " + fighter.LastName;
		Level.text = fighter.Level.ToString ();
		Race.text = fighter.Race.RaceName;
		Class.text = fighter.MyClass.Name;
		attack.text = fighter.Attack.ToString();
		defense.text = fighter.Defense.ToString();
		magic.text = fighter.Magic.ToString();
		strategy.text = fighter.Strategy.ToString();
		showcreatureship.text = fighter.Showcreatureship.ToString();
		comebackitude.text = fighter.Comebackitude.ToString();
	}

	void Update(){
		health.text = fighter.CurrentHealth.ToString();
		maxHealth.text = fighter.MaxHealth.ToString();
		energy.text = fighter.CurrentEnergy.ToString();
		maxEnergy.text = fighter.MaxEnergy.ToString();
		attackAdvance.text = fighter.AttackAdvance.ToString();
		defenseAdvance.text = fighter.DefenseAdvance.ToString();
		magicAdvance.text = fighter.MagicAdvance.ToString();
		strategyAdvance.text = fighter.StrategyAdvance.ToString();
		showcreatureshipAdvance.text = fighter.ShowcreatureshipAdvance.ToString();
		comebackitudeAdvance.text = fighter.ComebackitudeAdvance.ToString();
		experience.text = fighter.ExperiencePoints.ToString();

		if (fighter.CurrentActivity != null) {
			currentActivity.text = fighter.CurrentActivity.Name;
			activityAttackProgress.text = fighter.CurrentActivity.AttackIncrease.ToString();
			activityDefenseProgress.text = fighter.CurrentActivity.DefenseIncrease.ToString();
			activityMagicProgress.text = fighter.CurrentActivity.MagicIncrease.ToString();
			activityStrategyProgress.text = fighter.CurrentActivity.StrategyIncrease.ToString();
			activityShowcreatureshipProgress.text = fighter.CurrentActivity.ShowcreatureshipIncrease.ToString();
			activityComebackitudeProgress.text = fighter.CurrentActivity.ComebackitudeIncrease.ToString();
		} 
		else {
			currentActivity.text = "No Activity";

			activityAttackProgress.text = "0";
			activityDefenseProgress.text = "0";
			activityMagicProgress.text = "0";
			activityStrategyProgress.text = "0";
			activityShowcreatureshipProgress.text = "0";
			activityComebackitudeProgress.text = "0";
		}
	}
}
