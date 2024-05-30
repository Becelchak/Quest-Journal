using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Database")]

public class Database : ScriptableObject
{
    [SerializeField] List<TableQuestions> _questions;
    private List<NpcMain> NpcNamed;
    private Dictionary<string,NpcMain> NpcNames;
    private List<Quest> nowQuests;

    //[ContextMenu("Set ID")]
    //public void SetID()
    //{
    //    _questions = new List<TableQuestions>();

    //    var foundTable = Resources.LoadAll<TableQuestions>("Questions").OrderBy(i => i.ID).ToList();

    //    NpcNames.Add("NpcNamed",new FarmerSon());
    //}

    public void AddNowQuest(Quest quest)
    {
        nowQuests.Add(quest);
    }

    public void RemoveNowQuest(Quest quest)
    {
        nowQuests.Remove(quest);
    }
}
