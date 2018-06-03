using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardFunctions
{
	public static int PercentageChance()
	{
		//TODO: Make this more precise by operating on a 10000 scale, returning a float once reduced to a percentage scale?
		int roll = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(0, 101);

		return roll;
	}

	public static bool CheckPercentageChance(float chance)
	{
		int roll = PercentageChance();

		if (roll <= chance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
