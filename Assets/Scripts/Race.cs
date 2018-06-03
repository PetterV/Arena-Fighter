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
        if (RaceName == "Orc")
        {
            if (gender.NameUsage == "feminine")
            {
                firstNames = File.ReadAllLines("OrcFeminineFirstNames.txt");
            }
            else
            {
                firstNames = File.ReadAllLines("OrcMasculineFirstNames.txt");
            }
        }
		else if (RaceName == "Elf")
        {
            if (gender.NameUsage == "feminine")
            {
                firstNames = File.ReadAllLines("ElvesFeminineFirstNames.txt");
            }
            else
            {
                firstNames = File.ReadAllLines("ElvesMasculineFirstNames.txt");
            }
        }
		else if (RaceName == "Hobbit")
        {
            if (gender.NameUsage == "feminine")
            {
                firstNames = File.ReadAllLines("HobbitFeminineFirstNames.txt");
            }
            else
            {
                firstNames = File.ReadAllLines("HobbitMasculineFirstNames.txt");
            }
        }
		else if (RaceName == "Human")
        {
            if (gender.NameUsage == "feminine")
            {
                firstNames = File.ReadAllLines("HumanFeminineFirstNames.txt");
            }
            else
            {
                firstNames = File.ReadAllLines("HumanMasculineFirstNames.txt");
            }
        }
        //Fallback to human names
        else
        {
            firstNames = File.ReadAllLines("HumanFeminineFirstNames.txt");
        }
        return firstNames;
    }
    public string[] GetLastNames()
    {
        string[] lastNames;
		if (RaceName == "Elf")
        {
            lastNames = File.ReadAllLines("ElvenLastNames.txt");
        }
		else if (RaceName == "Hobbit")
        {
            lastNames = File.ReadAllLines("HobbitLastNames.txt");
        }
		else if (RaceName == "Orc")
        {
            lastNames = File.ReadAllLines("OrcLastNames.txt");
        }
        //Fallback to human names
        else
        {
            lastNames = File.ReadAllLines("HumanLastNames.txt");
        }
        return lastNames;
    }
}