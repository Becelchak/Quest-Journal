using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeManager : MonoBehaviour
{
    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
