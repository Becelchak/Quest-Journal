using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NpcMain;

[CreateAssetMenu(menuName = "NPC/ NamedNPC/ FarmerSon")]
public class FarmerSon : NpcMain
{
    public override int Age
    {
        get => base.Age;
        set { if (value > 17 && value < 110) base.Age = value; }
    }

    public FarmerSon() : base()
    {
        skills = new List<Skill>() { Skill.fight };
        base.Age = 16;
    }
}


