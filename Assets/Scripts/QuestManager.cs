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
                descriptionContent.text = "Сбор урожая для фермера\r\n\r\nФермер получил от предсказателя погоды предупреждение о том, что сезон дождей начнется раньше на несколько недель. Сам он не успеет собрать весь свой урожай до этого времени, так что ему срочно нужна помощь.";
                rangContent.text = "B";
                rewardContent.text = "7000 золотых монет";
                customerName.text = "Фермер";

                var image1 = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Material/PhotoBackground.png");
                customerPhoto.texture = image1;
                break;
            case QuestName.Pharmacist:
                descriptionContent.text = "Сбор ингредиентов для аптекаря\r\n\r\nАптекарю поступил срочный заказ на снадобья, для которых нужны некоторые ингредиенты, которых у него нет. Самому ему некогда собирать, а своих детей одних без присмотра тоже не хочет отпускать. т.к. боится, что они напортачат или с ними что-то случится.\r\n\r\nНужна ваша помощь.";
                rangContent.text = "C";
                rewardContent.text = "8050 золотых монет";
                customerName.text = "Аптекарь";

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
