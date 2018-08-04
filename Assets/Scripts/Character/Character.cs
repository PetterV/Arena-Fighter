using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Character //The character class
{
	public int ID { get; set; }
    public string FirstName { get; set; }
	public string LastName { get; set; }
	public Race Race { get; set; }
	public int Age { get; set; }
	public List<Trait> Traits { get; set; }
	public int Attack { get; set; }
	public int Defense { get; set; }
	public int Magic { get; set; }
	public int Strategy { get; set; }
	public int Showcreatureship { get; set; }
	public int Comebackitude { get; set; }
	public float MaxHealth { get; set; }
	public float CurrentHealth { get; set; }
	public Character CurrentTarget { get; set; }
	public float MaxEnergy { get; set; }
	public float CurrentEnergy { get; set; }
    public float FullCooldown { get; set; }
    public float CurrentCooldown { get; set; }
	public bool SecondWindActive { get; set; }
	public int SecondWindEnergyGain { get; set; }
	public bool UsedSpecialMoveThisTurn { get; set; }
	public int TurnsSinceLastSpecialMove { get; set; }
	public bool[] SpecialMovesKnown { get; set; }
	public int RandomBonusRange { get; set; }
	public int RandomEnergyBonusRange { get; set; }
	public int RandomHealthBonusRange { get; set; }
	public Class MyClass { get; set; }
	public int ExperiencePoints { get; set; }
	public int Level { get; set; }
	public int LevelUpThreshold { get; set; }
	public int AttackAdvance { get; set; }
	public int DefenseAdvance { get; set; }
	public int MagicAdvance { get; set; }
	public int StrategyAdvance { get; set; }
	public int ShowcreatureshipAdvance { get; set; }
	public int ComebackitudeAdvance { get; set; }
	public Activity CurrentActivity { get; set; }
	public int SkillAdvanceThreshold { get; set; }
	public Gender MyGender { get; set; }
	public bool IsInTeam { get; set; }
	public Team Team { get; set; }
	public int FightMoneyCut { get; set; }
	public FighterListButton FighterListButton { get; set; }
	public bool IsManager = false;
	public CharacterRelations Relations;
    public CharacterFlags CharacterFlags;

	public Character(int maxTraits, int minLevel, int maxLevel)
    {
		GameObject.Find ("GameController").GetComponent<GameController> ().CharacterDatabase.AssignID(this);
		SecondWindActive = false;
		SecondWindEnergyGain = 200;
		LevelUpThreshold = 1000;
		SkillAdvanceThreshold = 1000;
		//TODO: Replace Array here with dynamic list, and the demands for executing one with a matching string in list
		//Alternatively, a game object or class instance per special move?
		SpecialMovesKnown = new bool[2];
		RandomBonusRange = 5;
		RandomHealthBonusRange = 100;
		RandomEnergyBonusRange = 400;

		UsedSpecialMoveThisTurn = false;
		TurnsSinceLastSpecialMove = 0;
		IsInTeam = false;
        Traits = new List<Trait>();
        TraitList traitList = new TraitList();
        List<Trait> allTraits = traitList.GetTrait();
        RaceList races = new RaceList();
        List<Race> allRaces = races.GetRace();
        ClassList classes = new ClassList();
        List<Class> allClasses = classes.GetFClass();

        Level = 1;
        SetRace(allRaces);
        SetGender();
        SetAge();
        SetName();
        SetTraits(allTraits, maxTraits);
        SetFClass(allClasses);
        SetAllStats();
        SetBaseSpecialMoves();
        SetRandomFightMoneyCut();
        InitialLeveling(minLevel, maxLevel);
        CurrentHealth = MaxHealth;
        CurrentEnergy = MaxEnergy;

        ExperiencePoints = 0;

		Relations = new CharacterRelations ();
        CharacterFlags = new CharacterFlags();
    }

    public void SetAllStats()
    {
        SetAttack();
        SetDefense();
        SetMagic();
        SetStrategy();
        SetShowcreatureship();
        SetComebackitude();
        SetMaxHealth();
        SetMaxEnergy();
        SetCooldownValue();
    }

    public void PrintStats()
    {
		MonoBehaviour.print("Attack: " + Attack);
		MonoBehaviour.print("Defense: " + Defense);
		MonoBehaviour.print("Magic: " + Magic);
		MonoBehaviour.print("Strategy: " + Strategy);
		MonoBehaviour.print("Showcreatureship: " + Showcreatureship);
		MonoBehaviour.print("Comebackitude: " + Comebackitude);
        MonoBehaviour.print("Cooldown: " + FullCooldown);
    }

    public void SetAttack()
    {
        int attackBaseValue = 25;

        int traitAttackBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitAttackBonus = traitAttackBonus + trait.attack;
        }

        int attackBonus = traitAttackBonus + Race.AttackBonus + MyClass.Attack + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Attack = attackBaseValue + attackBonus;
    }
    public void SetDefense()
    {
        int defenseBaseValue = 25;
        int traitDefenseBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitDefenseBonus = traitDefenseBonus + trait.defense;
        }

        int defenseBonus = traitDefenseBonus + Race.DefenseBonus + MyClass.Defense + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Defense = defenseBaseValue + defenseBonus;
    }
    public void SetMagic()
    {
        int magicBaseValue = 25;
        int traitMagicBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitMagicBonus = traitMagicBonus + trait.magic;
        }

        int magicBonus = traitMagicBonus + Race.MagicBonus + MyClass.Magic + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Magic = magicBaseValue + magicBonus;
    }
    public void SetStrategy()
    {
        int strategyBaseValue = 25;
        int traitStrategyBonus = 0;
        foreach (Trait trait in Traits)
        {
            traitStrategyBonus = traitStrategyBonus + trait.strategy;
        }

        int strategyBonus = traitStrategyBonus + Race.StrategyBonus + MyClass.Strategy + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Strategy = strategyBaseValue + strategyBonus;
    }
    public void SetShowcreatureship()
    {
        int showcreatureshipBaseValue = 25;
        int traitShowcreatureshipBonus = 0;
        foreach (Trait trait in Traits)
        {
            traitShowcreatureshipBonus = traitShowcreatureshipBonus + trait.showcreatureship;
        }

        int showcreatureshipBonus = traitShowcreatureshipBonus + Race.ShowcreatureshipBonus + MyClass.Showcreatureship + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Showcreatureship = showcreatureshipBaseValue + showcreatureshipBonus;
    }
    public void SetComebackitude()
    {
        int comebackitudeBaseValue = 25;
        int traitComebackitudeBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitComebackitudeBonus = traitComebackitudeBonus + trait.comebackitude;
        }

        int comebackitudeBonus = traitComebackitudeBonus + Race.ComebackitudeBonus + MyClass.Comebackitude + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomBonusRange);
        Comebackitude = comebackitudeBaseValue + comebackitudeBonus;
    }

    public void SetMaxHealth()
    {
        float maxHealthBaseValue = 1000f;
        float traitMaxHealthBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitMaxHealthBonus = traitMaxHealthBonus + trait.health;
        }

        float maxHealthBonus = traitMaxHealthBonus + Race.HealthBonus + MyClass.Health + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomHealthBonusRange);
        MaxHealth = maxHealthBaseValue + maxHealthBonus;
    }
    public void SetMaxEnergy()
    {
        float maxEnergyBaseValue = 1000f;
        float traitMaxEnergyBonus = 0;

        foreach (Trait trait in Traits)
        {
            traitMaxEnergyBonus = traitMaxEnergyBonus + trait.energy;
        }

        float maxEnergyBonus = traitMaxEnergyBonus + Race.EnergyBonus + MyClass.Energy + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(RandomEnergyBonusRange);
        MaxEnergy = maxEnergyBaseValue + maxEnergyBonus;
    }

    public void SetCooldownValue()
    {
        float tempCooldown = BasicValues.baseCooldown;
        float modifiedStrategy = (float)Strategy * BasicValues.strategyCooldownFactor;
        tempCooldown = tempCooldown - modifiedStrategy;

        FullCooldown = tempCooldown;
    }

    public void SetRandomFightMoneyCut()
    {
        FightMoneyCut = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(BasicValues.minFightMoneyCut, BasicValues.maxFightMoneyCut + 1);
    }

    public void SelfIntroduction(string title, bool fullIntroduction)
    {
		MonoBehaviour.print("I am the " + title + ", " + FirstName + " " + LastName + ", a " + Age + " year old " + Race.RaceName + " " + MyClass.Name + ".");
        if (IsInTeam)
        {
			MonoBehaviour.print("My team is " + Team.TeamName);
        }
        else
        {
			MonoBehaviour.print("I am an independent fighter");
        }
        //PrintStats();
        if (fullIntroduction)
        {
			MonoBehaviour.print("I am known for being:");
            int traitsPrinted = 0;
            while (traitsPrinted < Traits.Count)
            {
				MonoBehaviour.print("  * " + Traits[traitsPrinted].traitName);
                traitsPrinted++;
            }

			MonoBehaviour.print("I have the following abilities:");
            if (SpecialMovesKnown[0])
				MonoBehaviour.print("  *Power Strike");
            if (SpecialMovesKnown[1])
				MonoBehaviour.print("  *Healing Spell");
        }

		MonoBehaviour.print("I have " + CurrentHealth + " Health");
		MonoBehaviour.print("I have " + CurrentEnergy + " Energy");
		MonoBehaviour.print("I am level " + Level + " and my stat total is " + CalculateTotalStats());
    }

    public void ActivateSecondWind()
    {
        SecondWindActive = true;

        //Make these values race-dependent? Class?
        CurrentEnergy = CurrentEnergy + SecondWindEnergyGain;
		MonoBehaviour.print(FirstName + " has gained a Second Wind!");
    }

    void SetGender()
    {
        GenderList genderList = new GenderList();
        List<Gender> allGenders = genderList.GetGender();
        //TODO: Make this weighted depending on race?
        int genderRoll = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(100);
        if(genderRoll < 6)
        {
            MyGender = allGenders.FirstOrDefault(o => o.Name == "nonbinary");
        }
        else if (genderRoll < 53)
        {
            MyGender = allGenders.FirstOrDefault(o => o.Name == "male");
        }
        else
        {
            MyGender = allGenders.FirstOrDefault(o => o.Name == "female");
        }
    }

    void SetName()
    {
        string[] firstNameList = new string[0];
        string[] lastNameList = new string[0];


        firstNameList = Race.GetFirstNames(MyGender);
        lastNameList = Race.GetLastNames();

        int totalFirstNames = firstNameList.Length;
        int totalLastNames = lastNameList.Length;
        int firstNameAssignment = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(totalFirstNames);
        int lastNameAssignment = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(totalLastNames);

        FirstName = firstNameList[firstNameAssignment];
        LastName = lastNameList[lastNameAssignment];
    }

    void SetTraits(List<Trait> allTraits, int maxTraits)
    {
        int numberOfPossibleTraits;
        int traitsSet = 0;
        int traitSelection;

        while (traitsSet < maxTraits)
        {
            numberOfPossibleTraits = allTraits.Count;
            traitSelection = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(numberOfPossibleTraits);
            Traits.Add(allTraits[traitSelection]);
            allTraits.RemoveAt(traitSelection);
            traitsSet++;
        }
    }

    void SetRace(List<Race> allRaces)
    {
        int numOfRaces = allRaces.Count;

        int raceSelection = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(numOfRaces);

        Race = allRaces[raceSelection];
    }

    void SetFClass(List<Class> allClasses)
    {
        int numOfClasses = allClasses.Count;

        int classSelection = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(numOfClasses);

        MyClass = allClasses[classSelection];
    }

    void SetAge()
    {
        int minAge = 0;
        int maxAge = 0;
        int peakAge = 0;

        minAge = Race.MinAge;
        maxAge = Race.MaxAge;
        peakAge = Race.PeakAge;

        int randomAge = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(minAge, maxAge); //Roll for inital age

        if (randomAge < peakAge) //Check difference from peak age, if lower have a chance of adjusting it up by half of the difference between set age and peak
        {
            int randomPeakDiff = peakAge - randomAge;
			bool adjust = StandardFunctions.CheckPercentageChance(50); //50% chance
            if (adjust)
            {
                int adjustValue = randomPeakDiff / 2;
                Age = randomAge + adjustValue;
            }
            else {
                Age = randomAge;
            }
        }
        else if (randomAge > peakAge) //If age is greater than peak age, chance of doing the opposite
        {
            int randomPeakDiff = randomAge - peakAge;
            bool adjust = StandardFunctions.CheckPercentageChance(50); //50% chance
            if (adjust)
            {
                int adjustValue = randomPeakDiff / 2;
                Age = randomAge + adjustValue;
            }
            else
            {
                Age = randomAge;
            }
        }
        else
        {
            Age = randomAge;
        }
    }

    void SetBaseSpecialMoves()
    {
        //TODO_P: Find a way of setting an appropriate number for these. Levels?
        int numberOfBaseMoves = 1;
        int movesAssigned = 0;

        while (numberOfBaseMoves > movesAssigned)
        {
            //TODO_P: Build a list of all valid special moves, and then select randomly an appropriate number from that list
            SpecialMoveReqs.PowerStrikeAssignment(this);
            SpecialMoveReqs.HealingSpellAssignment(this);
            movesAssigned++;
        }
    }

    void InitialLeveling(int minLevel, int maxLevel)
    {
        int actualMinLevel = minLevel - 1;
        int levelTarget = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(actualMinLevel, maxLevel);
        int levelsDone = 0;

        while (levelsDone < levelTarget)
        {
            LevelUp();
            levelsDone++;
        }
    }

    public void QuarterTickRecovery()
    {
        float defaultQuarterTickHealthRecovery = 3f;
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth = CurrentHealth + defaultQuarterTickHealthRecovery;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }

    public void YearlyAging()
    {
        Age = Age + 1;
		MonoBehaviour.print(FirstName + " is now " + Age + " years old.");
    }

    public int CalculateTotalStats()
    {
        int totalStats = Attack + Defense + Magic + Strategy + Showcreatureship + Comebackitude;
        return totalStats;
    }
    public float CalculateHealthEnergyValue()
    {
        float totalHealthEnergy = MaxHealth + MaxEnergy;
        return totalHealthEnergy;
    }

    public void GainExperience(int experienceGain)
    {
		MonoBehaviour.print(FirstName + " gained " + experienceGain + " experience!");
        int experienceCounted = 0;
        while (experienceCounted < experienceGain)
        {
            ExperiencePoints = ExperiencePoints + 1;
            experienceCounted++;
            if (ExperiencePoints >= LevelUpThreshold)
            {
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        Level = Level + 1;
		MonoBehaviour.print(FirstName + " is now level " + Level + "!");
        Attack = Attack + LevelUpSkillIncrease("attack");
        Defense = Defense + LevelUpSkillIncrease("defense");
        Magic = Magic + LevelUpSkillIncrease("magic");
        Strategy = Strategy + LevelUpSkillIncrease("strategy");
        Showcreatureship = Showcreatureship + LevelUpSkillIncrease("showcreatureship");
        Comebackitude = Comebackitude + LevelUpSkillIncrease("comebackitude");
        MaxHealth = MaxHealth + LevelUpHealthIncrease();
        MaxEnergy = MaxEnergy + LevelUpEnergyIncrease();
		if (StandardFunctions.CheckPercentageChance(BasicValues.fightMoneyCutLevelIncreaseChance))
        {
            FightMoneyCut++;
        }
        ExperiencePoints = 0;
    }

    int LevelUpSkillIncrease(string skill)
    {
        int skillIncrease = 0;

        //TODO: Better naming, since thresholds can be added dynamically
        double baseIncrease = 70;
        double singleIncrease = 50;
        double doubleIncrease = 30;
        double tripleIncrease = 25;
        double quadrupleIncrease = 5;
        double quintupleIncrease = 0.1;

        //This roll will increase the skill by 1 for each threshold it is below
        int skillIncreaseRoll = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(100);

        List<double> thresholds = new List<double>();

        if (skill == "attack")
        {
            baseIncrease = baseIncrease * MyClass.AttackFactor;
            singleIncrease = singleIncrease * MyClass.AttackFactor;
            doubleIncrease = doubleIncrease * MyClass.AttackFactor;
            tripleIncrease = tripleIncrease * MyClass.AttackFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.AttackFactor;
            quintupleIncrease = quintupleIncrease * MyClass.AttackFactor;
        }

        if (skill == "defense")
        {
            baseIncrease = baseIncrease * MyClass.DefenseFactor;
            singleIncrease = singleIncrease * MyClass.DefenseFactor;
            doubleIncrease = doubleIncrease * MyClass.DefenseFactor;
            tripleIncrease = tripleIncrease * MyClass.DefenseFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.DefenseFactor;
            quintupleIncrease = quintupleIncrease * MyClass.DefenseFactor;
        }

        if (skill == "magic")
        {
            baseIncrease = baseIncrease * MyClass.MagicFactor;
            singleIncrease = singleIncrease * MyClass.MagicFactor;
            doubleIncrease = doubleIncrease * MyClass.MagicFactor;
            tripleIncrease = tripleIncrease * MyClass.MagicFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.MagicFactor;
            quintupleIncrease = quintupleIncrease * MyClass.MagicFactor;
        }

        if (skill == "strategy")
        {
            baseIncrease = baseIncrease * MyClass.StrategyFactor;
            singleIncrease = singleIncrease * MyClass.StrategyFactor;
            doubleIncrease = doubleIncrease * MyClass.StrategyFactor;
            tripleIncrease = tripleIncrease * MyClass.StrategyFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.StrategyFactor;
            quintupleIncrease = quintupleIncrease * MyClass.StrategyFactor;
        }

        if (skill == "showcreatureship")
        {
            baseIncrease = baseIncrease * MyClass.ShowcreatureshipFactor;
            singleIncrease = singleIncrease * MyClass.ShowcreatureshipFactor;
            doubleIncrease = doubleIncrease * MyClass.ShowcreatureshipFactor;
            tripleIncrease = tripleIncrease * MyClass.ShowcreatureshipFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.ShowcreatureshipFactor;
            quintupleIncrease = quintupleIncrease * MyClass.ShowcreatureshipFactor;
        }

        if (skill == "comebackitude")
        {
            baseIncrease = baseIncrease * MyClass.ComebackitudeFactor;
            singleIncrease = singleIncrease * MyClass.ComebackitudeFactor;
            doubleIncrease = doubleIncrease * MyClass.ComebackitudeFactor;
            tripleIncrease = tripleIncrease * MyClass.ComebackitudeFactor;
            quadrupleIncrease = quadrupleIncrease * MyClass.ComebackitudeFactor;
            quintupleIncrease = quintupleIncrease * MyClass.ComebackitudeFactor;
        }

        //Add all the thresholds to a list
        thresholds.Add(baseIncrease);
        thresholds.Add(doubleIncrease);
        thresholds.Add(singleIncrease);
        thresholds.Add(tripleIncrease);
        thresholds.Add(quadrupleIncrease);
        thresholds.Add(quintupleIncrease);

        foreach (double d in thresholds)
        {
            if (skillIncreaseRoll <= d)
                skillIncrease++;
        }

        return skillIncrease;
    }

    float LevelUpHealthIncrease()
    {
        float healthIncrease;
        float baseHealthGainFactor = 50;
        float baseIncrease = Level * baseHealthGainFactor;
        //Account for class health factor
        baseIncrease = baseIncrease * MyClass.HealthFactor;

        //Premultiply
        float baseIncreasePremultiply = baseIncrease * 1000;
        int baseIncreaseInt = Convert.ToInt32(baseIncreasePremultiply);
        //Gain a minimum of half the possible increase
        int baseMinimumInt = baseIncreaseInt / 2;

        int healthIncreaseInt = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(baseMinimumInt, baseIncreaseInt);

        healthIncrease = healthIncreaseInt / 1000;

        return healthIncrease;
    }

    float LevelUpEnergyIncrease()
    {
        float baseEnergyGainFactor = 100;
        float baseIncrease = Level * baseEnergyGainFactor;
        //Account for class health factor
        baseIncrease = baseIncrease * MyClass.EnergyFactor;

        //Premultiply
        float baseIncreasePremultiply = baseIncrease * 1000;
        int baseIncreaseInt = Convert.ToInt32(baseIncreasePremultiply);
        //Gain a minimum of half the possible increase
        int baseMinimumInt = baseIncreaseInt / 2;

        int energyIncreaseInt = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(baseMinimumInt, baseIncreaseInt);

        float energyIncrease = energyIncreaseInt / 1000;

        return energyIncrease;
    }

    public void CheckForSkillIncrease()
    {
        if (AttackAdvance >= SkillAdvanceThreshold)
        {
            Attack++;
			MonoBehaviour.print(FirstName + ": My Attack increased to " + Attack);
        }

            if (DefenseAdvance >= SkillAdvanceThreshold)
        {
            Defense++;
			MonoBehaviour.print(FirstName + ": My Defense increased to " + Defense);
        }

            if (MagicAdvance >= SkillAdvanceThreshold)
        {
            Magic++;
			MonoBehaviour.print(FirstName + ": My Magic increased to " + Magic);
        }

        if (StrategyAdvance >= SkillAdvanceThreshold)
        {
            Strategy++;
			MonoBehaviour.print(FirstName + ": My Strategy increased to " + Strategy);
        }

        if (ShowcreatureshipAdvance >= SkillAdvanceThreshold)
        {
            Showcreatureship++;
			MonoBehaviour.print(FirstName + ": My Showcreatureship increased to " + Showcreatureship);
        }

        if (ComebackitudeAdvance >= SkillAdvanceThreshold)
        {
            Comebackitude++;
			MonoBehaviour.print(FirstName + ": My Comebackitude increased to " + Comebackitude);
        }
    }

	public void UpdateUI(){
		FighterListButton.UpdateFighterButtonInfo ();
	}

	public Manager MakeManager(Team team, Character fighter){
		this.IsManager = true;
		Manager Manager = new Manager(team, fighter);
		return Manager;
	}
}