using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrifManager : MonoBehaviour
{
    private List<Transform> questionesList = new List<Transform>();
    [SerializeField]
    private GameObject containerQuestions;
    [SerializeField]
    private InterviewManager interviewManager;
    [SerializeField]
    private GameObject startCell;

    void Start()
    {
        interviewManager = GetComponent<InterviewManager>();
        questionesList = interviewManager.GetListMainQuestions();
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
            var questionItem = interviewManager.nowQuests[indexQuestion];

            var question = cell.transform.GetChild(1);
            var answer = cell.transform.GetChild(2);

            question.GetComponent<Text>().text = questionItem.content;
            answer.GetComponent<Text>().text = questionItem.answer;
        }
    }

    public Quest FindQuestInfo(Transform cellTransfom)
    {
        var mainContainer = interviewManager.GetMainContainer();
        var questionNumber = mainContainer.transform.GetChild(int.Parse(cellTransfom.gameObject.name.Split('l')[2])).
            name.Split('(')[0].Split('l')[2];
        return interviewManager.GiveQuestFromList(questionNumber);
    }
}
