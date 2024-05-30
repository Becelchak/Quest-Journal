using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{

    [SerializeField]
    private Text pointCellName;
    [SerializeField]
    private Transform cellContainer;
    [SerializeField]
    private GameObject cell;
    [SerializeField]
    private GameObject applyButton;
    private bool isClosed;


    private BrifPoint point;
    private DragAndDrop dragAndDrop;
    public void OnDrop(PointerEventData eventData)
    {
        if(dragAndDrop == null) return;
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.position =
           transform.position;
        }

        if (dragAndDrop != null && !isClosed)
        {
            dragAndDrop.SetNewContainerParent(transform.GetChild(0).transform);
            //dragAndDrop.DeleteFromOldContainer();

            point = dragAndDrop.GiveQuestInfo().brifPoint;
            ChekQuestInfo();
            dragAndDrop.GetComponent<Collider2D>().enabled = false;

            applyButton.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        dragAndDrop = collision.GetComponent<DragAndDrop>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        dragAndDrop = null;
    }

    public void ChekQuestInfo()
    {
        if(point.ToString() == pointCellName.text)
        {
            var newCell = Instantiate(cell);
            newCell.transform.parent = cellContainer;

            newCell.GetComponentInChildren<Text>().text = dragAndDrop.GiveQuestInfo().brifPointContent;
            newCell.SetActive(true);
            newCell.transform.localScale = Vector3.one;
        }
    }

    public void CloseSlot()
    {
        isClosed = true;
    }
}
