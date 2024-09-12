using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : SingletonPattern.Singleton<InGameUI>
{
    [SerializeField] GameObject gameOverPanel;

    public void UIManagement(string command)
    {
        switch(command)
        {
            case "Retry":
                GameRetry();
                break;
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void GameRetry()
    {
        gameOverPanel.SetActive(true);
        GameManager.Instance.GameRetry();
    }
}
