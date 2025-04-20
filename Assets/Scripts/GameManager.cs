using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        SortingManager.OnCompleted += GotoNext;
        Timer.OnComplete += GotoNext;
    }

    private void OnDisable()
    {
        SortingManager.OnCompleted -= GotoNext;
        Timer.OnComplete -= GotoNext;
    }

    void GotoNext()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
