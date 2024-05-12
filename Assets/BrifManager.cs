using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrifManager : MonoBehaviour
{
    private List<Transform> QuestionesContainer = new List<Transform>();
    public GameObject ContainerQuestions;
    public InterviewManager InterviewManager;
    public GameObject startCell;

    void Start()
    {
        InterviewManager = GetComponent<InterviewManager>();
        QuestionesContainer = InterviewManager.GetListMainQuestions();
    }


    void Update()
    {
        QuestionesContainer = InterviewManager.GetListMainQuestions();

        if (ContainerQuestions.transform.childCount < QuestionesContainer.Count)
        {
            for (var i = ContainerQuestions.transform.childCount; i < QuestionesContainer.Count; i++)
            {
                var cell = Instantiate(startCell);
                cell.SetActive(true);
                cell.name = $"Cell{i}";
                cell.transform.parent = ContainerQuestions.transform;
                cell.transform.localScale = Vector3.one;
            }
        }

        if(InterviewManager.NowQuests.Count > 0)
        {
            for (var j = 0; j < ContainerQuestions.transform.childCount; j++)
            {
                var containerCell = ContainerQuestions.transform.GetChild(j);
                var indexQuestion = int.Parse(containerCell.name.Split('l')[2]);
                var questionItem = InterviewManager.NowQuests[indexQuestion];

                var question = containerCell.transform.GetChild(0);
                var answer = containerCell.transform.GetChild(1);

                question.GetComponent<Text>().text = questionItem.content;
                answer.GetComponent<Text>().text = questionItem.answer;

            }
        }

    }

    public void CleanContainer(int id)
    {
        var cell = ContainerQuestions.transform.GetChild(id);
        Destroy(cell.gameObject);
    }

}
