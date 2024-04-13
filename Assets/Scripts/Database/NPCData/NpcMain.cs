using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/ TestNPC")]
public class NpcMain : ScriptableObject
{
    public List<Skill> skills = new List<Skill>();
    public int age = 1;
    public virtual int Age
    {
        get => age;
        set { if (value > 0 && value < 110) age = value; }
    }
}

public enum Skill
{
    farmer = 0,
    speech = 1,
    fight = 2,
}
