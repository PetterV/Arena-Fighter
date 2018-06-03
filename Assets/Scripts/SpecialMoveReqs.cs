using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoveReqs : MonoBehaviour {

	public static bool HealingSpellReqs(Fighter fighter)
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

	static public void HealingSpellAssignment(Fighter fighter)
	{
		bool assign = HealingSpellReqs(fighter);
		if (assign)
		{
			fighter.SpecialMovesKnown[1] = true;
		}
	}

	public static bool PowerStrikeReqs(Fighter fighter)
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

	static public void PowerStrikeAssignment(Fighter fighter)
	{
		bool assign = PowerStrikeReqs(fighter);
		if (assign)
		{
			fighter.SpecialMovesKnown[0] = true;
		}
	}
}

