using UnityEngine;
using System.Collections;

public class LevelSystem {
	
    private static LevelSystem instance = new LevelSystem();

    private ParticleSystem levelUp;
    private int levelUpRequirement;
    public int LevelUpRequirement { get { return levelUpRequirement; } set { levelUpRequirement = value; } }
    private int level;
    public int Level { get { return level; } }

    private LevelSystem()
    {
        level = 1;
        levelUpRequirement = 2;
    }

    public static LevelSystem Instance
    {
        get
        {
            return instance;
        }
    }

    public void LevelIncrease()
    {
		levelUp = GameObject.Find ("Level_Up_Popup").GetComponent<ParticleSystem> ();
        levelUp.Stop();
        level++;
        levelUp.enableEmission = true;
        levelUp.Play();
    }

    public void LevelDecrease()
    {
        level--;
    }

}
