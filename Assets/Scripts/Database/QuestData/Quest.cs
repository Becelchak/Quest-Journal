using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Questions system/ Quest")]
public class Quest : ScriptableObject
{
    public int ID;
    public string content;
    public string answer;
    public Status questionStatus;
    public BrifPoint brifPoint;
    public string brifPointContent;
    public bool isHaveSpecialList;
    public string specialListContent;
    public SerializedDictionary<string, int> tags;
    private bool isUsedInBrif;
    public string hint;

    public void SetUsed()
    {
        isUsedInBrif = true;
    }

    public bool GetUseStatus()
    {
        return isUsedInBrif;
    }
}

public enum Status
{
    negative = -1,
    netural = 0,
    positive = 1,
}

public enum BrifPoint
{
    None = -1,
    ���� = 0,
    ������� = 1,
    ������� = 2,
    ���� = 3,
    �������� = 4,
    �������� = 5,
    ����� = 6,
    ����������� = 7,
}
