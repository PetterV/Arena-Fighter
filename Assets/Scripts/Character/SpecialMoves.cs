using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpecialMoves : MonoBehaviour
{
    //Special Move 0
    public static void PowerStrike(Character attacker, Character defender, Fight1v1 fight)
    {
        float powerStrikeBaseDamage = 60f;
        float armourBypass = 30f;

        attacker.TurnsSinceLastSpecialMove = 0;
        bool tempPrintStats = false;
		if (fight.PlayerInFight) {
			string powerStrikeReport = attacker.FirstName + " used Power Strike!";
			fight.fightPanel.UpdateTextLog (powerStrikeReport);

			tempPrintStats = true;
		}
        fight.CauseDamage(attacker, defender, fight, powerStrikeBaseDamage, armourBypass, true, tempPrintStats);
    }

    //Special Move 1
    public static void HealingSpell(Character attacker, Fight1v1 fight)
    {
        float healingBaseValue = 100f;

        //TODO_P: Prooobably going to need to rebalance this a lot.
        float healingValue = healingBaseValue + attacker.Magic;

		MonoBehaviour.print(attacker.FirstName + " used a healing spell!");
        attacker.CurrentHealth = attacker.CurrentHealth + healingValue;
    }
}
