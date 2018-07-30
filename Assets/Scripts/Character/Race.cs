using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class Race
{
    public string FirstNameFile { get; set; }
    public string LastNameFile { get; set; }

	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	public int PeakAge { get; set; }
	public int AttackBonus { get; set; }
	public int DefenseBonus { get; set; }
	public int MagicBonus { get; set; }
	public int StrategyBonus { get; set; }
	public int ShowcreatureshipBonus { get; set; }
	public int ComebackitudeBonus { get; set; }
	public string RaceNamePlural { get; set; }
	public string RaceName { get; set; }
	public float HealthBonus { get; set; }
	public float EnergyBonus { get; set; }

    public Race()
    {
        
    }

    public string[] GetFirstNames(Gender gender)
    {
        string[] firstNames;
		string endPath = "Text Files\\" + RaceName + gender.NameUsage + "FirstNames.txt";
		string filePath = Path.Combine (Application.streamingAssetsPath, endPath);

		if (File.Exists(filePath)){
			firstNames = File.ReadAllLines(filePath);
		}
        //Fallback to human names
        else
        {
			if (gender.NameUsage == "Masculine") {
				firstNames = File.ReadAllLines("HumanMasculineFirstNames.txt");
			}
			else {
				firstNames = File.ReadAllLines("HumanFeminineFirstNames.txt");	
			}
        }
        return firstNames;
    }
    public string[] GetLastNames()
    {
        string[] lastNames;
		string endPath = "Text Files\\" + RaceName + "LastNames.txt";
		string filePath = Path.Combine (Application.streamingAssetsPath, endPath);

		if (File.Exists(filePath)){
			lastNames = File.ReadAllLines(filePath);
		}
		//Fallback to human names
		else
		{
			lastNames = File.ReadAllLines("HumanLastNames.txt");
		}
        return lastNames;
    }
}