using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Date {

	public int Day = 0;
	public int Month = 0;
	public int Year = 0;
	public int WeekCounter = 0;
	public int MonthCounter = 0;

	public void SetTodaysDate()
	{
		Day = GameObject.Find("GameController").GetComponent<GameController>().RealDate.Day;
		Month = GameObject.Find("GameController").GetComponent<GameController>().RealDate.Month;
		Year = GameObject.Find("GameController").GetComponent<GameController>().RealDate.Year;
	}

	public void IncreaseDate(int daysToIncrease)
	{
		int daysIncreased = 0;
		while (daysIncreased < daysToIncrease)
		{
			if (Day == 31)
			{
				if (Month == 12)
				{
					Year++;
					Month = 1;
				}
				else
				{
					Month++;
				}
				Day = 1;
			}
			else
			{
				Day++;
			}
			daysIncreased++;
		}
	}

	public void PrintDate(){
		MonoBehaviour.print ("Day: " + Day + "\nMonth: " + Month + "\nYear: " + Year);
	}
}
