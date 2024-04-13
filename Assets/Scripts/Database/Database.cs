using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Database : ScriptableObject
{
    [SerializeField] List<TableQuestions> _questions;

    [ContextMenu("Set ID")]
    public void SetID()
    {
        _questions = new List<TableQuestions>();

        var foundTable = Resources.LoadAll<TableQuestions>("Questions").OrderBy(i => i.ID).ToList();
    }
}
