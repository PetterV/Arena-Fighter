using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoveReqs : MonoBehaviour {

	public static bool HealingSpellReqs(Character fighter)
	{
		if (fighter.Magic > 35)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	static public void HealingSpellAssignment(Character fighter)
	{
		bool assign = HealingSpellReqs(fighter);
		if (assign)
		{
			fighter.SpecialMovesKnown[1] = true;
		}
	}

	public static bool PowerStrikeReqs(Character fighter)
	{
		if(fighter.Attack > 35)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	static public void PowerStrikeAssignment(Character fighter)
	{
		bool assign = PowerStrikeReqs(fighter);
		if (assign)
		{
			fighter.SpecialMovesKnown[0] = true;
		}
	}
}

