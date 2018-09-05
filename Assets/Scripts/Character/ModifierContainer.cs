using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierContainer {

    public GameController gameController;
    public List<CharacterModifier> modifierList = new List<CharacterModifier>();

    // Use this for initialization
    public ModifierContainer()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void AddCharacterModifier(string type, int duration)
    {
        modifierList.Add(new CharacterModifier(type, duration, gameController));
    }

    public void RemoveCharacterModifier(CharacterModifier modifier)
    {
        modifierList.Remove(modifier);
    }

    public void CheckDurations()
    {
        foreach (CharacterModifier m in modifierList)
        {
            if (m.duration >= gameController.DaysProgressed)
            {
                //TODO: Notifications for some kind of message system
                RemoveCharacterModifier(m);
            }
        }
    }

    public class CharacterModifier
    {
        public string modifierType;
        public bool injury = false;
        public int duration;
        public int attackImpact = 0;
        public int defenceImpact = 0;
        public int magicImpact = 0;
        public int strategyImpact = 0;
        public int showcreatureshipImpact = 0;
        public int comebackitudeImpact = 0;

        private GameController gameController;

        public CharacterModifier(string type, int modifierDuration, GameController gameController)
        {
            List<CharacterModifierItem> modifierTypes = GameObject.Find("ModifierManager").GetComponent<ModifierManager>().CharacterModifierTypes;
            modifierType = type;
            foreach (CharacterModifierItem m in modifierTypes)
            {
                if (type == modifierType) {
                    attackImpact = m.attackImpact;
                    defenceImpact = m.defenseImpact;
                    magicImpact = m.magicImpact;
                    strategyImpact = m.strategyImpact;
                    showcreatureshipImpact = m.showcreatureshipImpact;
                    comebackitudeImpact = m.comebackitudeImpact;
                    break;
                }
            }
            duration = gameController.DaysProgressed + modifierDuration;
        }
    }
}
