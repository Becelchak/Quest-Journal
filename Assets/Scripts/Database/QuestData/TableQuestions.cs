using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Questions system/ TableQuestions")]
public class TableQuestions : ScriptableObject
{
    public int ID;
    public List<Quest> Questions;

    public Quest GetQuest(int id)
    {
        return Questions[id];
    }
}
