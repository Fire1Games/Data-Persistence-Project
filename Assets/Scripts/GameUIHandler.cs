using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{

    public Text HiScoreTextInGame;
    public GameManager GameManager;

    void Start()
    {
        if (SaveInfo.Instance != null)
        {
            HiScoreTextInGame.text = "Best Score : " + SaveInfo.Instance.HiScorePlayer + " : " + SaveInfo.Instance.HiScore;
        }
        else
        {
            HiScoreTextInGame.text = "Problem Loading Best Score";
        }
    }

    public void UpdateHiScore()
    {
        if (SaveInfo.Instance != null)
        {
            HiScoreTextInGame.text = "Best Score : " + SaveInfo.Instance.HiScorePlayer + " : " + SaveInfo.Instance.HiScore;
        }
        else
        {
            HiScoreTextInGame.text = "Best Score : " + GameManager.NameText.text + " : " + GameManager.m_Points;
        }
    }
}
