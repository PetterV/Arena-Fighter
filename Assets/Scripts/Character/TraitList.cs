using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class TraitList
{
	private List<Trait> traitList;
	public List<Trait> GetTrait()
	{
		if (traitList == null)
			LoadXml();
		return traitList;
	}

	private void LoadXml()
	{
		string filePath = Path.Combine (Application.streamingAssetsPath, "Character Data\\PersonalityTraits.xml");
		traitList =
			(
				from t in XDocument.Load(filePath).Root.Elements("trait")
				select new Trait
				{
					traitName = (string)t.Element("name"),
					attack = (int)t.Element("attack"),
					defense = (int)t.Element("defense"),
					magic = (int)t.Element("magic"),
					strategy = (int)t.Element("strategy"),
					showcreatureship = (int)t.Element("showcreatureship"),
					comebackitude = (int)t.Element("comebackitude"),
					health = (float)t.Element("health"),
					energy = (float)t.Element("energy")
				}

			).ToList();
	}
}