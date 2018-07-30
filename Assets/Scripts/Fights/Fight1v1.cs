using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Fight1v1
{
	//Consider moving more fight handling into Unity
	public GameController gameController { get; set; }
	public int TickCount { get; set; }
	public bool FightOver { get; set; }
	public float LastDamageDone { get; set; }
	public bool PlayerInFight { get; set; }
	public bool PlayerWatchingFight { get; set; }
	public Audience Audience { get; set; }
	public string FirstFighterTitle { get; set; }
	public string SecondFighterTitle { get; set; }
	public GameObject FightPlanButton { get; set; }
	public FightPanel fightPanel { get; set; }
	public InputController inputController { get; set; }
	float f1damageToReport = 0;
	float f2damageToReport = 0;
	Character f1;
	Character f2;

	public Fight1v1(Character fighter1, Character fighter2, string fightType, GameObject fightPlanButton)
    {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		inputController = GameObject.Find ("InputController").GetComponent<InputController> ();
		TickCount = 1;
		f1 = fighter1;
		f2 = fighter2;
		FightPlanButton = fightPlanButton;
        Audience = new Audience();
		if (f1.Team == gameController.PlayerManager.PlayerTeam || f2.Team == GameObject.Find("GameController").GetComponent<GameController>().PlayerManager.PlayerTeam)
        {
            PlayerInFight = true;
			PlayerWatchingFight = true;
        }

		FirstFighterTitle = SetTitleBasedOnType(f1, false, fightType);
        f1.SelfIntroduction(FirstFighterTitle, PlayerInFight);
        SecondFighterTitle = SetTitleBasedOnType(f1, true, fightType);
        f2.SelfIntroduction(SecondFighterTitle, PlayerInFight);

		if (!PlayerWatchingFight) {
			while (!FightOver) {
				FightRoundTick ();
			}
			gameController.UnPauseTicks ();
		}
		else {
			gameController.UIController.GetComponent<UIController> ().ActivateFightScreen (f1, f2, this);
		}

        f1.CurrentEnergy = f1.MaxEnergy;
        f2.CurrentEnergy = f2.MaxEnergy;
		if (PlayerWatchingFight) {
			fightPanel.UpdateTextLog (f1.FirstName + " " + f1.LastName + " will fight " + f2.FirstName + " " + f2.LastName);
		}
	}

	public void FightRoundTick(){
		FightTick (f1, f2, Audience);
		Audience.RegularAudienceAppreciationAdjustment (f1, f2, LastDamageDone, false);
		CheckForVictory1v1 (f1, f2);
		FightTick (f2, f1, Audience);
		CheckForVictory1v1 (f1, f2);
		Audience.RegularAudienceAppreciationAdjustment (f2, f1, LastDamageDone, false);

		//Report damage every ten ticks
		if (TickCount > 9 && TickCount % 5 == 0 && !FightOver) {
			if (PlayerWatchingFight) {
				string attackReport = f1.FirstName + " did " + f1damageToReport + " damage to " + f2.FirstName;
				fightPanel.UpdateTextLog (attackReport);
				f1damageToReport = 0;
				attackReport = f2.FirstName + " did " + f2damageToReport + " damage " + f1.FirstName;
				fightPanel.UpdateTextLog (attackReport);
				f2damageToReport = 0;
				fightPanel.UpdateTextLog ("This was tick " + TickCount);
				fightPanel.fighterBox1.UpdateHealthAndEnergy ();
				fightPanel.fighterBox2.UpdateHealthAndEnergy ();
			}
		}

		if (TickCount > 19 && TickCount % 20 == 0 && !FightOver && PlayerWatchingFight) {
		//	fightPanel.UpdateTextLog("The audience has " + Audience.AudienceAppreciation + "appreciation for this fight.");
		}

		if(TickCount >= 120)
		{
			if (PlayerWatchingFight) {
				fightPanel.UpdateTextLog ("The fight is at and end. A draw!");
				fightPanel.UpdateTextLog (f1.FirstName + " has " + f1.CurrentHealth + " health left.");
				fightPanel.UpdateTextLog (f2.FirstName + " has " + f2.CurrentHealth + " health left.");
			}
			f1.GainExperience(400);
			f2.GainExperience(400);
			FightManager.PayOut(Audience, f2, f1, true);
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (null);
		}

		TickCount++;
	}

  

    public void CheckForVictory1v1(Character f1, Character f2)
    {
        if (f1.CurrentHealth <= 0)
        {
			if (PlayerWatchingFight) {
				fightPanel.UpdateTextLog(f1.FirstName + " " + f1.LastName + " is down!");
				fightPanel.UpdateTextLog(f2.FirstName + " has " + f2.CurrentHealth + " health left.");
				fightPanel.UpdateTextLog(f2.FirstName + " " + f2.LastName + " wins!");
			}
			f2.GainExperience(500);
            f1.GainExperience(250);
            FightManager.PayOut(Audience, f2, f1, false);
            FightOver = true;
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (f2);
        }
        else if (f2.CurrentHealth <= 0)
        {
			if (PlayerWatchingFight) {
				fightPanel.UpdateTextLog(f2.FirstName + " " + f2.LastName + " is down!");
				fightPanel.UpdateTextLog(f1.FirstName + " has " + f1.CurrentHealth + " health left.");
				fightPanel.UpdateTextLog(f1.FirstName + " " + f1.LastName + " wins!");
			}
            f1.GainExperience(500);
            f2.GainExperience(250);
            FightManager.PayOut(Audience, f1, f2, false);
			FightOver = true;
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (f1);
        }
    }

	public void FightTick (Character attacker, Character defender, Audience audience)
	{
		//TODO_P: Find a good location for this. One of the GameControl managers?
		float baseDamage = 20;
		float baseEnergyCost = 100;

		if (attacker.CurrentHealth / attacker.MaxHealth < 0.33 && !attacker.SecondWindActive) //Chance of receiving a Second Wind
		{
			SecondWindChance(attacker, audience);
		}

		//Chance of using a special move
		SpecialMoveChance(attacker, defender, this);

		//If no special move was used this turn, cause normal damage
		//TODO_P: Find solution for this making characters do no damage on the first turn.
		if(attacker.TurnsSinceLastSpecialMove > 0)
		{
			StandardAttack(attacker, defender, this, baseDamage, baseEnergyCost);
		}
		attacker.TurnsSinceLastSpecialMove++;
	}

	public string SetTitleBasedOnType(Character fighter, bool challenger, string fightType)
	{
		string title = null;
		if (fightType == "championship_match")
		{
			if (challenger)
			{
				title = "challenger";
			}
			else
			{
				title = "champion";
			}
		}
		return title;
	}

	public void StandardAttack(Character attacker, Character defender, Fight1v1 fight, float baseDamage, float baseEnergyCost)
	{
		CauseDamage(attacker, defender, fight, baseDamage, 0f, false, false);
		//Calculate actual energy cost:
		PayEnergyCost(attacker, baseEnergyCost);
	}

	public void CauseDamage(Character attacker, Character defender, Fight1v1 fight, float baseDamage, float armourBypass, bool ignoreEnergyFactor, bool tempPrintStats)
	{
		float damageToDeal;
		float minEnergyFactor = 0.1f;

		float secondWindDamageFactor = 1.0f;
		float secondWindDefenseFactor = 1.0f;

		float minDamage = 10;
		float attackEnergyFactor;
		int baseDefense = 1;
		float defenseEnergyFactor;
		int damagePrecisionFactor = 100;

		//Calculate max possible defense:
		//Todo: Why is this all done as int?
		int maxPossibleDefenseIntValue = baseDefense + defender.Defense - Convert.ToInt32(armourBypass);
		if (maxPossibleDefenseIntValue < baseDefense)
		{
			maxPossibleDefenseIntValue = baseDefense;
		}

		//Reduce by the defenseEnergyFactor:
		defenseEnergyFactor = defender.CurrentEnergy / defender.MaxEnergy;
		//Make sure that there's a lower limit to the defense
		if (defenseEnergyFactor < minEnergyFactor)
		{
			defenseEnergyFactor = minEnergyFactor;
		}
		float maxPossibleDefenseFloatValue = maxPossibleDefenseIntValue * defenseEnergyFactor * damagePrecisionFactor;
		//Convert to int for the random:
		maxPossibleDefenseIntValue = Convert.ToInt32(maxPossibleDefenseFloatValue);

		int minPossibleDefenseInt = maxPossibleDefenseIntValue / 2;
		//Roll for final defense value:
		float actualDefense = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(minPossibleDefenseInt, maxPossibleDefenseIntValue);
		//Increase in case of second wind
		if (defender.SecondWindActive)
		{
			actualDefense = actualDefense * secondWindDefenseFactor;
		}
		//Convert back to a float in a reasonable range:
		actualDefense = actualDefense / damagePrecisionFactor;

		//Calculate maximum possible damage:
		float maxPossibleDamage = baseDamage + attacker.Attack - actualDefense;
		//If actualDefense is greater than the possible damage, cause a minimum amount of damage:
		if (maxPossibleDamage <= 0) 
		{
			damageToDeal = minDamage;
		}
		else
		{
			//Convert maxPossibleDamage to int and calculate minPossibleDamage:
			float maxPossibleDamageIntValue = maxPossibleDamage * damagePrecisionFactor;
			int maxPossibleDamageInt = Convert.ToInt32(maxPossibleDamageIntValue);

			int minPossibleDamageInt = maxPossibleDamageInt / 2;
			//Roll for random actual damage:
			int damageToDealIntValue = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(minPossibleDamageInt, maxPossibleDamageInt);
			//Convert back to float damage
			damageToDeal = damageToDealIntValue / damagePrecisionFactor;
		}
		//If the damage is lower than the minimum, set it to the minimum.
		if (damageToDeal < minDamage)
		{
			damageToDeal = minDamage;
		}
		//Reduce resulting damage by the energy factor
		attackEnergyFactor = attacker.CurrentEnergy / attacker.MaxEnergy;
		//Cap minimum attack energy factor somewhere:
		if (attackEnergyFactor < minEnergyFactor &! ignoreEnergyFactor)
		{
			attackEnergyFactor = minEnergyFactor;
		}
		else
		{
			attackEnergyFactor = 1;
		}
		damageToDeal = damageToDeal * attackEnergyFactor;

		//Increase in case of Second Wind
		if (attacker.SecondWindActive)
		{
			damageToDeal = damageToDeal * secondWindDamageFactor;
		}

		//TODO: Some weird damage factor number goes here, in case that is a better way to balance the power curve than increasing health

		//Modifiers
		if (PlayerWatchingFight){
			if (fightPanel.fighterBox1.fighter == attacker && fightPanel.fighterBox1.extraDamageModifier || fightPanel.fighterBox2.fighter == attacker && fightPanel.fighterBox2.extraDamageModifier) {
				damageToDeal = damageToDeal + gameController.GetComponent<FightModifiers> ().DamageIncrease (damageToDeal, 1);
			}
		}
		//Deal the damage to the defender
		defender.CurrentHealth = defender.CurrentHealth - damageToDeal;

		if (PlayerWatchingFight) {
			if (tempPrintStats == true) {
				fightPanel.UpdateTextLog ("Power Strike caused " + damageToDeal + " damage!");
			} else {
				if (attacker == f1) {
					f1damageToReport = f1damageToReport + damageToDeal;
				} else {
					f2damageToReport = f2damageToReport + damageToDeal;
				}
			}
		}
		//Update details on button:
		attacker.UpdateUI();
		defender.UpdateUI();

		//Store the last damage done in the Fight itself
		fight.LastDamageDone = damageToDeal;
	}

	public void PayEnergyCost (Character fighter, float cost)
	{
		float minEnergyCost = 5;
		//Convert a Fighter's Strategy into %
		float energyCostReductionFactor = fighter.Strategy / 100f;
		//Calculate value to reduce energy cost by
		float realEnergyCostReduction = cost * energyCostReductionFactor;
		//Calculate the final cost
		float finalCost = cost - realEnergyCostReduction;
		//Set to a minimum value, if necessary
		if (finalCost < minEnergyCost)
		{
			finalCost = minEnergyCost;
		}

		//Subtract the final cost
		fighter.CurrentEnergy = fighter.CurrentEnergy - finalCost;
		if(fighter.CurrentEnergy < 0)
		{
			fighter.CurrentEnergy = 0;
		}
	}

	public void SecondWindChance(Character fighter, Audience audience)
	{
		float comebackitudeChance = fighter.Comebackitude / 10f;
		bool activateSecondWind = StandardFunctions.CheckPercentageChance(comebackitudeChance);
		if (activateSecondWind)
		{
			fighter.ActivateSecondWind();
			if (PlayerWatchingFight) {
				string secondWindActivation = fighter.FirstName + " has activated " + fighter.MyGender.HerHis + " Second Wind!";
				fightPanel.UpdateTextLog (secondWindActivation);
			}
		}
	}

	public void SpecialMoveChance(Character attacker, Character defender, Fight1v1 fight)
	{
		bool activateSpecialMove = false;
		float specialChanceDivisonFactor = 1000f;
		float fighterSpecialChanceValue = attacker.Magic + attacker.Strategy;
		float fighterSpecialChance = fighterSpecialChanceValue / specialChanceDivisonFactor;

		activateSpecialMove = StandardFunctions.CheckPercentageChance(fighterSpecialChance);

		if (activateSpecialMove)
		{
			//TODO_P: Add a system for handling selection of special moves
			SpecialMoves.PowerStrike(attacker, defender, fight);
		}
	}

	public void StopWatchingFight (){
		PlayerInFight = false;
		while (TickCount < 120) {
			FightRoundTick ();
			if (FightOver)
				break;
		}
	}
}