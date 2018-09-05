using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuryContainer
{
    public GameController gameController;
    public List<Injury> injuryList = new List<Injury>();

    // Use this for initialization
    public InjuryContainer() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void AddInjury (string type, int duration)
    {
        injuryList.Add(new Injury(type, duration, gameController));
    }

    public void RemoveInjury (Injury injury)
    {
        injuryList.Remove(injury);
    }

    public void CheckDurations()
    {
        foreach (Injury i in injuryList)
        {
            if (i.duration >= gameController.DaysProgressed)
            {
                //TODO: Notifications for some kind of message system
                RemoveInjury(i);
            }
        }
    }

    public class Injury {
        public string injuryType;
        public int duration;
        private GameController gameController;

        public Injury(string type, int injuryDuration, GameController gameController)
        {
            injuryType = type;
            duration = gameController.DaysProgressed + injuryDuration;
        }
    }
}
