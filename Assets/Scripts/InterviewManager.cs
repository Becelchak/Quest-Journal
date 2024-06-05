using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour
{
    //  онтейнеры с €чейками вопросов. Main контейнер - это контейнер правой страницы с ограничени€ми
    private List<Transform> questionesContainer = new List<Transform>();
    private List<Transform> questionsMainContainer = new List<Transform>();

    [SerializeField]
    private GameObject containerAllQuestions;
    [SerializeField]
    private TableQuestions questions;
    [SerializeField]
    private GameObject containerMainQuestions;
    [SerializeField]
    private GameObject countMainQuestions;
    // Ћист вопросов в данный момент вписанных в Main контейнер
    public List<Quest> nowQuestsInterview;
    void Start()
    {
        // ѕроводим заполнение списка заготовленными €чейками, чтобы хватило место под все вопросы в рамках квеста
        if (containerAllQuestions.transform.childCount < questions.Questions.Count)
        {
            for (var i = 1; i < questions.Questions.Count; i++)
            {
                var cell = Instantiate(containerAllQuestions.transform.GetChild(0).gameObject);
                cell.name = $"Cell{i}";
                cell.transform.parent = containerAllQuestions.transform;
                cell.transform.localScale = Vector3.one;
            }
        }
        // ѕроводим заполнение €чеек(Cell) содержанием вопросов
        for(var j = 0; j < containerAllQuestions.transform.childCount; j++)
        {
            var containerCell = containerAllQuestions.transform.GetChild(j);
            questionesContainer.Add(containerCell);
            containerCell.GetComponentInChildren<Text>().text = questions.GetQuest(questionesContainer.Count - 1).content;

            var count = questionsMainContainer.Count;
            countMainQuestions.GetComponent<Text>().text = $"{count}/5";
        }

    }

    // ƒобавл€ем вопрос из общего списка в список выбранных вопросов дл€ интервью
    public void AddQuestInList(GameObject question)
    {
        if (questionsMainContainer.Contains(question.transform) || questionsMainContainer.Count == 5)
        {
            question.GetComponent<Button>().interactable = true;
            return;
        }
        question.GetComponent<Button>().interactable = false;
        var cell = Instantiate(question);
        cell.transform.parent = containerMainQuestions.transform;
        cell.transform.localScale = Vector3.one;

        cell.GetComponent<Button>().interactable = false;
        cell.transform.GetChild(1).gameObject.SetActive(true);
        questionsMainContainer.Add(cell.transform);

        var count = questionsMainContainer.Count;
        countMainQuestions.GetComponent<Text>().text = $"{count}/5";

        var id = int.Parse(cell.name.Split('l')[2].Split('(')[0]);
        nowQuestsInterview.Add(questions.GetQuest(id));

    }

    // ”дал€ем вопрос из списка вопросов дл€ интервью
    public void RemoveQuestFromList(GameObject question)
    {
        if (!questionsMainContainer.Contains(question.transform)) return;
        var cell = containerMainQuestions.transform.GetChild(questionsMainContainer.IndexOf(question.transform));
        var nameCell = cell.name.Split('(')[0];
        var cellInAllList = GameObject.Find(nameCell);
        cellInAllList.GetComponent<Button>().interactable = true;

        var id = int.Parse(cell.name.Split('l')[2].Split('(')[0]);
        GetComponent<BrifManager>().CleanContainer(nowQuestsInterview.IndexOf(questions.GetQuest(id)));
        nowQuestsInterview.Remove(questions.GetQuest(id));
        Destroy(cell.gameObject);
        questionsMainContainer.Remove(question.transform);

        var count = questionsMainContainer.Count;
        countMainQuestions.GetComponent<Text>().text = $"{count}/5";

    }

    public void RemoveQuestFromList(int id)
    {
        var cell = containerMainQuestions.transform.GetChild(id);
        RemoveQuestFromList(cell.gameObject);

    }
    public List<Transform> GetListMainQuestions()
    {
        return questionsMainContainer;
    }

    public GameObject GetMainContainer()
    {
        return containerMainQuestions;
    }

    public Quest GiveQuestFromList(string questNumber)
    {
        Quest quest = null;
        foreach (var question in nowQuestsInterview) 
        {
            if(question.name.Contains(questNumber))
                quest = question;
        }
        return quest;
    }

    public void SetClampMainQuestions()
    {
        foreach (var item in questionsMainContainer)
        {
            item.GetChild(1).gameObject.SetActive(false);
        }
    }
}
