using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class GenderList
{
	private List<Gender> genderList;
	public List<Gender> GetGender()
	{
		if (genderList == null)
			LoadXml();
		return genderList;
	}

	private void LoadXml()
	{
		genderList =
			(
				from t in XDocument.Load("Genders.xml").Root.Elements("gender")
				select new Gender
				{
					Name = (string)t.Element("name"),
					NameCap = (string)t.Element("nameCap"),
					NameUsage = (string)t.Element("nameUsage"),
					SheHe = (string)t.Element("SheHe"),
					SheHeCap = (string)t.Element("SheHeCap"),
					HerHim = (string)t.Element("HerHim"),
					HerHimCap = (string)t.Element("HerHimCap"),
					HerHis = (string)t.Element("HerHis"),
					HerHisCap = (string)t.Element("HerHisCap"),
					HersHis = (string)t.Element("HersHis"),
					HersHisCap = (string)t.Element("HersHisCap"),
					HerselfHimself = (string)t.Element("HerselfHimself"),
					HerselfHimselfCap = (string)t.Element("HerselfHimselfCap"),
				}

			).ToList();
	}
}
