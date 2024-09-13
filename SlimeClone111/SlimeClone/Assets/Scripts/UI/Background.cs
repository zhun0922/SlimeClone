using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Background : SingletonPattern.Singleton<Background>
{
    public float Speed { get; private set; } = GameManager.FixedSpeed;

    [SerializeField] int startIndex;
    [SerializeField] int endIndex;
    [SerializeField] Transform[] sprites;

    float viewHeight;

    bool isPuaseMove = false;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        isPuaseMove = false;
    }

    private void Update()
    {
        if (!isPuaseMove)
        {
            Vector3 curPos = transform.position;
            Vector3 nextPos = Vector3.down * Speed * Time.deltaTime;
            transform.position = curPos + nextPos;

            if (sprites[endIndex].position.y < viewHeight * (-1))
            {
                Vector3 backSpritePos = sprites[startIndex].localPosition;
                Vector3 frontSpritePos = sprites[endIndex].localPosition;

                sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 10;

                //이동완료되면 EndIndex, StartIndex갱신
                int startIndexSave = startIndex;
                startIndex = endIndex;
                endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            }
        }
    }

    public void PauseMove()
    {
        isPuaseMove = true;
    }

    public void ResumeMove()
    {
        isPuaseMove = false;
    }
}
