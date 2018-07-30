using System;
using System.Collections.Generic;
using UnityEngine;


public class ActivityManager
{
    public void PickRandomActivity(Character f)
    {
        ActivityList activityList = new ActivityList();
        List<Activity> allActivities = activityList.GetActivity();
        int numberOfActivities = allActivities.Count;

		int selectedActivity = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(numberOfActivities);

        f.CurrentActivity = allActivities[selectedActivity];
    }
}