using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class RaceList
{
	private List<Race> raceList;
	public List<Race> GetRace()
	{
		if (raceList == null)
			LoadXml();
		return raceList;
	}

	private void LoadXml()
	{
		raceList =
			(
				from t in XDocument.Load("Races.xml").Root.Elements("race")
				select new Race
				{
					RaceName = (string)t.Element("name"),
					RaceNamePlural = (string)t.Element("namePlural"),
					MinAge = (int)t.Element("MinAge"),
					MaxAge = (int)t.Element("MaxAge"),
					PeakAge = (int)t.Element("PeakAge"),
					AttackBonus = (int)t.Element("AttackBonus"),
					DefenseBonus = (int)t.Element("DefenseBonus"),
					MagicBonus = (int)t.Element("MagicBonus"),
					StrategyBonus = (int)t.Element("StrategyBonus"),
					ShowcreatureshipBonus = (int)t.Element("ShowcreatureshipBonus"),
					ComebackitudeBonus = (int)t.Element("ComebackitudeBonus"),
					HealthBonus = (float)t.Element("HealthBonus"),
					EnergyBonus = (float)t.Element("EnergyBonus"),
					FirstNameFile = (string)t.Element("firstnamefile"),
					LastNameFile = (string)t.Element("lastnamefile")
				}

			).ToList();
	}
}

