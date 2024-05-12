using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/ TestNPC")]
public class NpcMain : ScriptableObject
{
    public List<Skill> skills = new List<Skill>();
    public int age = 1;
    public List<Actions> actions = new List<Actions>();
    public virtual int Age
    {
        get => age;
        set { if (value > 0 && value < 110) age = value; }
    }
}

public enum Skill
{
    farming = 0,
    speech = 1,
    fight = 2,
}

public enum Actions
{
    take = 0,
    find = 1,
    talk = 2,
    harvest = 3,
}