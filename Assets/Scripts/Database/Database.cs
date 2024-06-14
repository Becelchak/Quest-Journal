using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Database")]

public class Database : ScriptableObject
{
    [SerializeField] List<TableQuestions> _questions = new List<TableQuestions>();
    private List<NpcMain> NpcNamed;
    private Dictionary<string,NpcMain> NpcNames;
    [SerializeField]
    private List<Quest> nowQuests = new List<Quest>();
    public void ClearNowQuestList()
    {
        nowQuests.Clear();
    }

    public void AddNowQuest(Quest quest)
    {
        if(!nowQuests.Contains(quest))
            nowQuests.Add(quest);
    }

    public void RemoveNowQuest(Quest quest)
    {
        nowQuests.Remove(quest);
    }

    public TableQuestions GetTableQuestions(int number)
    {
        return _questions[number];
    }

    public List<Quest> GetNowQuest()
    {
        return nowQuests;
    }
}
