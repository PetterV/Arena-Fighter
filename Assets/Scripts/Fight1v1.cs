using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Fight1v1
{
	//Consider moving more fight handling into Unity
	public int TickCount { get; set; }
	public bool FightOver { get; set; }
	public float LastDamageDone { get; set; }
	public bool PlayerInFight { get; set; }
	public Audience Audience { get; set; }
	public string FirstFighterTitle { get; set; }
	public string SecondFighterTitle { get; set; }
	public GameObject FightPlanButton { get; set; }
	Fighter f1;
	Fighter f2;

	public Fight1v1(Fighter fighter1, Fighter fighter2, string fightType, GameObject fightPlanButton)
    {
		GameController gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		f1 = fighter1;
		f2 = fighter2;
		FightPlanButton = fightPlanButton;
        Audience = new Audience();
		if (f1.Team == gameController.PlayerManager.PlayerTeam || f2.Team == GameObject.Find("GameController").GetComponent<GameController>().PlayerManager.PlayerTeam)
        {
            PlayerInFight = true;
        }

		FirstFighterTitle = SetTitleBasedOnType(f1, false, fightType);
        f1.SelfIntroduction(FirstFighterTitle, PlayerInFight);
        SecondFighterTitle = SetTitleBasedOnType(f1, true, fightType);
        f2.SelfIntroduction(SecondFighterTitle, PlayerInFight);

		if (!PlayerInFight) {
			while (TickCount < 120) {
				FightRoundTick ();
				TickCount++;
				if (FightOver)
					break;
			}
		} 
		else {
			gameController.UIController.GetComponent<UIController> ().ActivateFightScreen (f1, f2, this);
		}

        f1.CurrentEnergy = f1.MaxEnergy;
        f2.CurrentEnergy = f2.MaxEnergy;
    }

	public void FightRoundTick(){
		FightTick (f1, f2, Audience);
		Audience.RegularAudienceAppreciationAdjustment (f1, f2, LastDamageDone, false);
		CheckForVictory1v1 (f1, f2);
		FightTick (f2, f1, Audience);
		CheckForVictory1v1 (f1, f2);
		Audience.RegularAudienceAppreciationAdjustment (f2, f1, LastDamageDone, false);

		if (TickCount % 10 == 0) {
			//MonoBehaviour.print ("The audience has " + Audience.AudienceAppreciation + "appreciation for this fight.");
		}

		if(TickCount >= 120)
		{
			MonoBehaviour.print("The fight is at and end. A draw!");
			MonoBehaviour.print(f1.FirstName + " has " + f1.CurrentHealth + " health left.");
			MonoBehaviour.print(f2.FirstName + " has " + f2.CurrentHealth + " health left.");
			f1.GainExperience(400);
			f2.GainExperience(400);
			FightManager.PayOut(Audience, f2, f1, true);
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (null);
		}
	}

  

    public void CheckForVictory1v1(Fighter f1, Fighter f2)
    {
        if (f1.CurrentHealth <= 0)
        {
			//MonoBehaviour.print(f1.FirstName + " " + f1.LastName + " is down!");
			//MonoBehaviour.print(f2.FirstName + " has " + f2.CurrentHealth + " health left.");
			//MonoBehaviour.print(f2.FirstName + " " + f2.LastName + " wins!");
            f2.GainExperience(500);
            f1.GainExperience(250);
            FightManager.PayOut(Audience, f2, f1, false);
            FightOver = true;
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (f2);
        }
        else if (f2.CurrentHealth <= 0)
        {
			//MonoBehaviour.print(f2.FirstName + " " + f2.LastName + " is down!");
			//MonoBehaviour.print(f1.FirstName + " has " + f1.CurrentHealth + " health left.");
			//MonoBehaviour.print(f1.FirstName + " " + f1.LastName + " wins!");
            f1.GainExperience(500);
            f2.GainExperience(250);
            FightManager.PayOut(Audience, f1, f2, false);
			FightOver = true;
			FightPlanButton.GetComponent<FightPlanListButton> ().FightHasBeen (f1);
        }
    }

	public void FightTick (Fighter attacker, Fighter defender, Audience audience)
	{
		//TODO_P: Find a good location for this.
		float baseDamage = 15;
		float baseEnergyCost = 50;

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

	public string SetTitleBasedOnType(Fighter fighter, bool challenger, string fightType)
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

	public void StandardAttack(Fighter attacker, Fighter defender, Fight1v1 fight, float baseDamage, float baseEnergyCost)
	{
		CauseDamage(attacker, defender, fight, baseDamage, 0f, false, false);
		//Calculate actual energy cost:
		PayEnergyCost(attacker, baseEnergyCost);
	}

	public void CauseDamage(Fighter attacker, Fighter defender, Fight1v1 fight, float baseDamage, float armourBypass, bool ignoreEnergyFactor, bool tempPrintStats)
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

		//Deal the damage to the defender
		defender.CurrentHealth = defender.CurrentHealth - damageToDeal;

		if (tempPrintStats == true)
			MonoBehaviour.print("Power Strike caused " + damageToDeal + " damage!");

		//Update details on button:
		attacker.UpdateUI();
		defender.UpdateUI();

		//Store the last damage done in the Fight itself
		fight.LastDamageDone = damageToDeal;
	}

	public void PayEnergyCost (Fighter fighter, float cost)
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

	public void SecondWindChance(Fighter fighter, Audience audience)
	{
		float comebackitudeChance = fighter.Comebackitude / 10f;
		bool activateSecondWind = StandardFunctions.CheckPercentageChance(comebackitudeChance);
		if (activateSecondWind)
		{
			fighter.ActivateSecondWind();
		}
	}

	public void SpecialMoveChance(Fighter attacker, Fighter defender, Fight1v1 fight)
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
}