using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class BrifManager : MonoBehaviour
{
    private List<Transform> questionesList = new List<Transform>();
    [SerializeField]
    private GameObject containerQuestions;
    [SerializeField]
    private InterviewManager interviewManager;
    [SerializeField]
    private GameObject startCell;

    [SerializeField]
    private Database database;
    // Определяет пороговое значение правильных вопросов для прохождения на следующий этап
    [SerializeField]
    private double percentCorrectQuestionsExample = 50;
    public List<string> hints = new List<string>();

    void Start()
    {
        interviewManager = GetComponent<InterviewManager>();
        questionesList = interviewManager.GetListMainQuestions();
        database.ClearNowQuestList();
    }


    void Update()
    {
        questionesList = interviewManager.GetListMainQuestions();
    }

    public void CleanContainer(int id)
    {
        if(containerQuestions.transform.childCount > 0)
        {
            var cell = containerQuestions.transform.GetChild(id);
            Destroy(cell.gameObject);
        }
    }

    // Вызывается если список выбранных вопросов был изменен
    public void ChangeList()
    {

        // Проверяет равенство ячеек в Gameobject контейнере на странице брифа и списка активных вопросов
        if (containerQuestions.transform.childCount < questionesList.Count)
        {
            for (var i = containerQuestions.transform.childCount; i < questionesList.Count; i++)
            {
                var cell = Instantiate(startCell);
                cell.SetActive(true);
                cell.name = $"Cell{i}";
                cell.transform.parent = containerQuestions.transform;
                cell.transform.localScale = Vector3.one;
            }
        }

        // В случае новых ячеек пеерименовывает все ячейки согласно их положению как дочернего объекта, помогает избежать ошибки при обращении от списка вопросов к контейнеру
        for (var h = 0; h < containerQuestions.transform.childCount; h++)
        {
            var cell = containerQuestions.transform.GetChild(h);
            cell.name = $"Cell{h}";
        }

        // Наполняет каждую ячейку вопросом и ответом на него
        for (var j = 0; j < containerQuestions.transform.childCount; j++)
        {
            var cell = containerQuestions.transform.GetChild(j);
            var indexQuestion = int.Parse(cell.name.Split('l')[2]);
            var questionItem = interviewManager.nowQuestsInterview[indexQuestion];

            var question = cell.transform.GetChild(1);
            var answer = cell.transform.GetChild(2);

            question.GetComponent<Text>().text = questionItem.content;
            answer.GetComponent<Text>().text = questionItem.answer;

            if(questionItem.hint != "" && !hints.Contains(questionItem.hint))
                hints.Add(questionItem.hint);
        }
        var hintList = GameObject.Find("HintList");
        var noteExample = hintList.transform.GetChild(0);
        var container = hintList.transform.GetChild(1);

        foreach (var hint in hints)
        {
            var note = Instantiate(noteExample);
            note.transform.SetParent(container);
            note.gameObject.SetActive(true);
            note.transform.localScale = Vector3.one;

            note.GetComponentInChildren<Text>().text = hint;
            
        }
    }

    public Quest FindQuestInfo(Transform cellTransfom)
    {
        var mainContainer = interviewManager.GetMainContainer();
        var questionNumber = mainContainer.transform.GetChild(int.Parse(cellTransfom.gameObject.name.Split('l')[2])).
            name.Split('(')[0].Split('l')[2];
        return interviewManager.GiveQuestFromList(questionNumber);
    }

    public void ApplyBrifText(GameObject cell)
    {
        var cellName = cell.transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.name.Split(' ')[1];
        GameObject BrifCell = null; 
        switch (cellName)
        {
            case "employer":
                BrifCell = GameObject.Find("Employer text");
                break;
            case "deadline":
                BrifCell = GameObject.Find("Deadline text");
                break;
            case "target":
                BrifCell = GameObject.Find("Target text");
                break;
            case "quality":
                BrifCell = GameObject.Find("Quality text");
                break;
            case "specialist":
                BrifCell = GameObject.Find("Specialist text");
                break;
            case "risks":
                BrifCell = GameObject.Find("Risks text");
                break;
        }
        var str = new StringBuilder();
        str.Append(BrifCell.GetComponent<Text>().text);
        str.Append($"{cell.GetComponentInChildren<Text>().text};");
        BrifCell.GetComponent<Text>().text = str.ToString();

        var tempList = cell.transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.transform.GetChild(1).
            GetComponent<Slot>().GetApplyQuestList();
        var index = cell.transform.GetSiblingIndex();
        database.AddNowQuest(tempList[index]);
    }

    public void DeleteBrifText(GameObject cell) 
    {
        var cellName = cell.transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.name.Split(' ')[1];
        GameObject BrifCell = null;
        switch (cellName)
        {
            case "employer":
                BrifCell = GameObject.Find("Employer text");
                break;
            case "deadline":
                BrifCell = GameObject.Find("Deadline text");
                break;
            case "target":
                BrifCell = GameObject.Find("Target text");
                break;
            case "quality":
                BrifCell = GameObject.Find("Quality text");
                break;
            case "specialist":
                BrifCell = GameObject.Find("Specialist text");
                break;
            case "risks":
                BrifCell = GameObject.Find("Risks text");
                break;
        }

        var output = BrifCell.GetComponent<Text>().text
                .Split($"{cell.GetComponentInChildren<Text>().text};");
        BrifCell.GetComponent<Text>().text = string.Join("",output);

        var tempList = cell.transform.parent.
            transform.parent.
            transform.parent.
            transform.parent.transform.GetChild(1).
            GetComponent<Slot>().GetApplyQuestList();
        var index = cell.transform.GetSiblingIndex();
        database.RemoveNowQuest(tempList[index]);
    }

    public void CalculateBrif()
    {
        var allCorrectQuest = 0.0;
        var correctNumber = 0.0;
        var incorrectNumber = 0.0;
        var table = database.GetTableQuestions(0);
        foreach ( Quest quest in table.Questions)
        {
            if(quest.questionStatus == Status.positive)
                allCorrectQuest++;
        }

        foreach(var quest in database.GetNowQuest())
        {
            if(quest.questionStatus == Status.positive)
                correctNumber++;
            else if(quest.questionStatus == Status.negative)
                incorrectNumber++;
        }

        var totalPercentCorrect = correctNumber / allCorrectQuest * 100;
        var totalPercentIncorrect = incorrectNumber / allCorrectQuest * 100;

        GameObject.Find("Statistic text").GetComponent<Text>().text = string.Format("{0}% правильно \n {1}% неправильно", totalPercentCorrect,totalPercentIncorrect);

        if(totalPercentCorrect >= percentCorrectQuestionsExample)
        {
            GameObject.Find("Sucsess text").GetComponent<Text>().enabled = true;
            var canvasGroupButton = GameObject.Find("Button next stage").GetComponent<CanvasGroup>();

            canvasGroupButton.alpha = 1.0f;
            canvasGroupButton.interactable = true;
            canvasGroupButton.blocksRaycasts = true;
        }
        else
        {
            GameObject.Find("Fail text").GetComponent<Text>().enabled = true;
            var canvasGroupButton = GameObject.Find("Button return menu").GetComponent<CanvasGroup>();

            canvasGroupButton.alpha = 1.0f;
            canvasGroupButton.interactable = true;
            canvasGroupButton.blocksRaycasts = true;
        }
    }
}
