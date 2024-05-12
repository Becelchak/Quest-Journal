using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour
{
    private List<Transform> QuestionesContainer = new List<Transform>();
    private List<Transform> QuestionsMainContainer = new List<Transform>();

    public GameObject ContainerAllQuestions;
    public TableQuestions Questions;
    public GameObject ContainerMainQuestions;

    public GameObject CountMainQuestions;
    public List<Quest> NowQuests;
    void Start()
    {
        // ѕроводим заполнение списка заготовленными €чейками, чтобы хватило место под все вопросы в рамках квеста
        if (ContainerAllQuestions.transform.childCount < Questions.Questions.Count)
        {
            for (var i = 1; i < Questions.Questions.Count; i++)
            {
                var cell = Instantiate(ContainerAllQuestions.transform.GetChild(0).gameObject);
                cell.name = $"Cell{i}";
                cell.transform.parent = ContainerAllQuestions.transform;
                cell.transform.localScale = Vector3.one;
            }
        }
        // ѕроводим заполнение €чеек(Cell) содержанием вопросов
        for(var j = 0; j < ContainerAllQuestions.transform.childCount; j++)
        {
            var containerCell = ContainerAllQuestions.transform.GetChild(j);
            QuestionesContainer.Add(containerCell);
            containerCell.GetComponentInChildren<Text>().text = Questions.GetQuest(QuestionesContainer.Count - 1).content;

            var count = QuestionsMainContainer.Count;
            CountMainQuestions.GetComponent<Text>().text = $"{count}/5";
        }

    }

    // ƒобавл€ем вопрос из общего списка в список выбранных вопросов дл€ интервью
    public void AddQuestInList(GameObject question)
    {
        if (QuestionsMainContainer.Contains(question.transform) || QuestionsMainContainer.Count == 5)
        {
            question.GetComponent<Button>().interactable = true;
            return;
        }
        question.GetComponent<Button>().interactable = false;
        var cell = Instantiate(question);
        cell.transform.parent = ContainerMainQuestions.transform;
        cell.transform.localScale = Vector3.one;

        cell.GetComponent<Button>().interactable = false;
        cell.transform.GetChild(1).gameObject.SetActive(true);
        QuestionsMainContainer.Add(cell.transform);

        var count = QuestionsMainContainer.Count;
        CountMainQuestions.GetComponent<Text>().text = $"{count}/5";

        var id = int.Parse(cell.name.Split('l')[2].Split('(')[0]);
        NowQuests.Add(Questions.GetQuest(id));
    }

    // ”дал€ем вопрос из списка вопросов дл€ интервью
    public void RemoveQuestFromList(GameObject question)
    {
        if (!QuestionsMainContainer.Contains(question.transform)) return;
        var cell = ContainerMainQuestions.transform.GetChild(QuestionsMainContainer.IndexOf(question.transform));
        var nameCell = cell.name.Split('(')[0];
        var cellInAllList = GameObject.Find(nameCell);
        cellInAllList.GetComponent<Button>().interactable = true;

        var id = int.Parse(cell.name.Split('l')[2].Split('(')[0]);
        GetComponent<BrifManager>().CleanContainer(NowQuests.IndexOf(Questions.GetQuest(id)));
        NowQuests.Remove(Questions.GetQuest(id));
        Destroy(cell.gameObject);
        QuestionsMainContainer.Remove(question.transform);

        var count = QuestionsMainContainer.Count;
        CountMainQuestions.GetComponent<Text>().text = $"{count}/5";
    }

    public List<Transform> GetListMainQuestions()
    {
        return QuestionsMainContainer;
    }
}
