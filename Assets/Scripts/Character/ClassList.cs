using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class ClassList
{
	private List<Class> fClassList;
	public List<Class> GetFClass()
	{
		if (fClassList == null)
			LoadXml();
		return fClassList;
	}

	private void LoadXml()
	{
		string filePath = Path.Combine (Application.streamingAssetsPath, "Character Data\\Classes.xml");
		fClassList =
			(
				from t in XDocument.Load(filePath).Root.Elements("class")
				select new Class
				{
					Name = (string)t.Element("name"),
					Path = (string)t.Element("path"),
					Stage = (int)t.Element("stage"),
					NaturalAge = (int)t.Element("natural_age"),
					Attack = (int)t.Element("attack"),
					Defense = (int)t.Element("defense"),
					Magic = (int)t.Element("magic"),
					Strategy = (int)t.Element("strategy"),
					Showcreatureship = (int)t.Element("showcreatureship"),
					Comebackitude = (int)t.Element("comebackitude"),
					Health = (float)t.Element("health"),
					Energy = (float)t.Element("energy"),
					AttackFactor = (double)t.Element("attack_factor"),
					DefenseFactor = (double)t.Element("defense_factor"),
					MagicFactor = (double)t.Element("magic_factor"),
					StrategyFactor = (double)t.Element("strategy_factor"),
					ShowcreatureshipFactor = (double)t.Element("showcreatureship_factor"),
					ComebackitudeFactor = (double)t.Element("comebackitude_factor"),
					HealthFactor = (float)t.Element("health_factor"),
					EnergyFactor = (float)t.Element("energy_factor")
				}

			).ToList();
	}
}
