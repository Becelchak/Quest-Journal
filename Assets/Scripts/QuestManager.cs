using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Text descriptionContent;
    [SerializeField] private Text rangContent;
    [SerializeField] private Text rewardContent;
    [SerializeField] private Text customerName;
    [SerializeField] private RawImage customerPhoto;

    public QuestName typeQuestPlaceholder;
    void Start()
    {
        //if(customerPhoto == null)
        //{

        //    var defaultPhoto = AssetDatabase.LoadAssetAtPath<RawImage>("Assets/Material/PhotoDefault.png");
        //}

        switch (typeQuestPlaceholder) {
            case QuestName.FarmerQuest:
                descriptionContent.text = "���� ������ ��� �������\r\n\r\n������ ������� �� �������������������� ��������������� ���, ��� ����� ������ �������� ������ �� ��������� ������. ��� ���� ������ ������������ ���� �������� ����� �������, ��� ��� ��� ������ ����� ������.";
                rangContent.text = "B";
                rewardContent.text = "7000 ������� �����";
                customerName.text = "������";

                var image1 = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Material/PhotoBackground.png");
                customerPhoto.texture = image1;
                break;
            case QuestName.Pharmacist:
                descriptionContent.text = "���� ������������ ��� ��������\r\n\r\n�������� �������� ������� ����� �� ��������, ��� ������� ����� ��������� �����������, ������� � ���� ���. ������ ��� ������� ��������, � ����� ����� ����� ��� ��������� ���� �� ����� ���������. �.�. ������, ��� ��� ���������� ��� � ���� ���-�� ��������.\r\n\r\n����� ���� ������.";
                rangContent.text = "C";
                rewardContent.text = "8050 ������� �����";
                customerName.text = "��������";

                var image2 = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Material/PhotoDefault.png");
                customerPhoto.texture = image2;
                break;
            default:
                break;
        }
    }

    public enum QuestName
    {
       FarmerQuest = 0,
       Pharmacist = 1

    }
}
