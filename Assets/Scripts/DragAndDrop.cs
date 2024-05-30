using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    private Transform parentAfterDrag;
    private Transform primalParent;

    [SerializeField]
    private InterviewManager interviewManager;
    [SerializeField]
    private BrifManager brifManager;
    [SerializeField] 
    private GameObject cellPrefab;

    private Quest questInfo;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        questInfo = brifManager.FindQuestInfo(transform);
        primalParent = transform.parent;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(rectTransform, false);
        image.color = new Color(0f, 255f, 200f, 0.7f);
        image.raycastTarget = false;


        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        rectTransform.localScale = new Vector3(0.5f, 0.3f, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta * 1.8f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Если родителем определен список из таблицы -> создаем новую ячейку и определяем её родителем контейнер внутри списка на фиксированном месте
        // Прежнюю ячейку удаляем
        if(parentAfterDrag.tag == "TableList")
        {
            var newCell = Instantiate(cellPrefab);
            newCell.gameObject.SetActive(true);
            newCell.transform.SetParent(parentAfterDrag.transform.GetChild(0));
            newCell.name = $"CellTable{parentAfterDrag.transform.childCount - 1}";

            var answer = transform.GetChild(2).GetComponent<Text>();
            newCell.transform.GetChild(1).GetComponent<Text>().text = answer.text;
            newCell.transform.localScale = new Vector3(1f, 1f, 1f);

            transform.parent = primalParent;
        }
        else
        {
            image.color = new Color(255f, 255f, 255f, 1f);
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);

            rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    public void SetNewContainerParent(Transform newContainer)
    {
        parentAfterDrag = newContainer;
    }

    public void DeleteFromOldContainer()
    {
        //interviewManager.RemoveQuestFromList(int.Parse(gameObject.name.Split('l')[2]));
        brifManager.ChangeList();
    }

    public Quest GiveQuestInfo()
    {
        return questInfo;
    }
}
