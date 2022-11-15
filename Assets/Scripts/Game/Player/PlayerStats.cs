using UnityEngine;
using TMPro;
using Mirror;

public class PlayerStats : NetworkBehaviour
{
    public int ID;
    [SerializeField] private int maxScore = 10;
    [SyncVar(hook = nameof(changeText)), HideInInspector] public int Score = 0;
    [SyncVar] public int Wins = 0;
    private TMP_Text scoreText;

    void Start()
    {
        scoreText = transform.parent.GetComponent<GoalReference>().ScoreText;
        GameManager.GameState += resetScore;
    }

    public void SetID(int id)
    {
        this.ID = id;
    }

    [Server]
    public void DoScore()
    {
        Score++;
        if (Score >= maxScore)
        {
            Wins ++;
            GameObject.Find("Manager").GetComponent<GameManager>().EndGame();
            return;
        }
        GameObject.Find("Manager").GetComponent<GameManager>().TimerPanel.SetActive(true);
    }

    [Server]
    private void resetScore(bool value)
    {
        if (value)
            Score = 0;
    }

    void changeText(int oldValue, int newValue)
    {
        scoreText.text = newValue.ToString();
        GameObject.Find("Manager").GetComponent<GameManager>().TimerPanel.SetActive(true);
    }

    void OnDisable()
    {
        GameManager.GameState -= resetScore;
    }
}