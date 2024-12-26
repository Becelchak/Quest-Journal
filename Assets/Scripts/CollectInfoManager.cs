using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UI.Tables;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CollectInfoManager : MonoBehaviour
{
    [SerializeField] private Database db;
    [SerializeField] private TableLayout tableForInfo;

    private GameObject cellExample;
    void Start()
    {
        cellExample = tableForInfo.Rows[0].Cells[0].transform.GetChild(0).gameObject;
        //for(var i =1; i < tableForInfo.Rows.Count; i++) 
        //{
        //    tableForInfo.Rows[i].ClearCells();
        //}
    }

    public void RefreshTableInfo()
    {
        foreach(var quest in db.GetNowQuest())
        {
            var target = quest.brifPoint;

            tableForInfo.AddRow();

            foreach (var cell in tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells) 
            {
                //var textCell = Instantiate(cellExample);
                var сell = GameObject.Instantiate(cellExample);
                сell.transform.parent = cell.transform;
                cell.transform.GetChild(0).transform.localScale = Vector3.one;

                var textCellTMP = cell.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                textCellTMP.text = "-";
                textCellTMP.autoSizeTextContainer = true;
            }

            Transform container = tableForInfo.Rows[0].Cells[0].transform;
            switch (target)
            {
                case BrifPoint.Цель:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[0].transform.GetChild(0).transform.GetChild(0);
                    break;
                case BrifPoint.Ресурсы:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[3].transform.GetChild(0).transform.GetChild(0);
                    break;
                case BrifPoint.Срок:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[1].transform.GetChild(0).transform.GetChild(0);
                    break;
                case BrifPoint.Качество:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[2].transform.GetChild(0).transform.GetChild(0);
                    break;
                case BrifPoint.Риски:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[4].transform.GetChild(0).transform.GetChild(0);
                    break;
                case BrifPoint.Специалисты:
                    container = tableForInfo.Rows[tableForInfo.Rows.Count - 1].Cells[5].transform.GetChild(0).transform.GetChild(0);
                    break;
                default:
                    break;
            }
            // Main text
            container.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.brifPointContent;
            // Hint text
            var hintText = container.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            if(quest.hint != "")
            {
                hintText.text = quest.hint;
                hintText.transform.parent.gameObject.SetActive(true);
                // More info image
                var infoImage = container.transform.GetChild(2);
                infoImage.gameObject.SetActive(true);
            }

        }
    }

}
