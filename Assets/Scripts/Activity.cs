using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity
{
	public string Name { get; set; }
	public int AttackIncrease { get; set; }
	public int DefenseIncrease { get; set; }
	public int MagicIncrease { get; set; }
	public int StrategyIncrease { get; set; }
	public int ShowcreatureshipIncrease { get; set; }
	public int ComebackitudeIncrease { get; set; }

	public void AttendingActivityTick(Character f)
	{
		f.AttackAdvance = f.AttackAdvance + AttackIncrease;
		f.DefenseAdvance = f.DefenseAdvance + DefenseIncrease;
		f.MagicAdvance = f.MagicAdvance + MagicIncrease;
		f.StrategyAdvance = f.StrategyAdvance + StrategyIncrease;
		f.ShowcreatureshipAdvance = f.ShowcreatureshipAdvance + ShowcreatureshipIncrease;
		f.ComebackitudeAdvance = f.ComebackitudeAdvance + ComebackitudeIncrease;
	}
}
