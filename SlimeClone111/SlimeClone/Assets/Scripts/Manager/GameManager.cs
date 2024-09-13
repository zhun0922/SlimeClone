using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPattern.Singleton<GameManager>
{
    [SerializeField] GameObject playerObj;
    public GameObject PlayerObj => playerObj;

    public Player Player;

    public const float FixedSpeed = 3f;

    public bool IsPauseMove { get; set; } = false;
    public bool HasReborn { get; set; } = false;

    private void Awake()
    {
        Player = playerObj.GetComponent<Player>();
        ResumeAllObjs();
    }


    public void PauseAllObjs()
    {
        EnemyManager.Instance.PauseMove();
        AugmentManager.Instance.PauseMove();
        Background.Instance.PauseMove();
    }

    public void ResumeAllObjs()
    {
        EnemyManager.Instance.ResumeMove();
        AugmentManager.Instance.ResumeMove();
        Background.Instance.ResumeMove();
    }

    public void PauseMove()
    {
        IsPauseMove = true;
        PauseAllObjs();
    }

    public void ResumeMove()
    {
        IsPauseMove = false;
        ResumeAllObjs();
    }

    public void GameOver()
    {
        InGameUI.Instance.GameOver();
        PauseMove();
        Debug.Log("게임종료");
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
        Debug.Log("게임재개");
        //ResumeMove();
    }

    public float ElapsedTime { get; private set; } = 0f; 

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
    }
}
