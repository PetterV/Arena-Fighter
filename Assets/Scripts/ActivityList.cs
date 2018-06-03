using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class ActivityList
{
	private List<Activity> activityList;
	public List<Activity> GetActivity()
	{
		if (activityList == null)
			LoadXml();
		return activityList;
	}

	private void LoadXml()
	{
		activityList =
			(
				from t in XDocument.Load("Activities.xml").Root.Elements("activity")
				select new Activity
				{
					Name = (string)t.Element("name"),
					AttackIncrease = (int)t.Element("attack"),
					DefenseIncrease = (int)t.Element("defense"),
					MagicIncrease = (int)t.Element("magic"),
					StrategyIncrease = (int)t.Element("strategy"),
					ShowcreatureshipIncrease = (int)t.Element("showcreatureship"),
					ComebackitudeIncrease = (int)t.Element("comebackitude")
				}

			).ToList();
	}
}
