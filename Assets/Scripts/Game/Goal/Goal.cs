using UnityEngine;
using System;

public class Goal : MonoBehaviour
{
    public int GoalID = 0;

    [SerializeField] private bool twoPlayers;
    [SerializeField] private PlayerManager pM;
    public static event Action<Goal, bool> ScoreInGoal;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Ball"))
            ScoreInGoal?.Invoke(this, twoPlayers);
    }

    public void ScoreTo(PlayerStats playerStats)
    {
        if (playerStats != null)
        {
            playerStats.DoScore();
            return;
        }
        
        PlayerStats[] pSArray = pM.GetPlayersStats();
        foreach (PlayerStats ps in pSArray)
            if (GoalID != ps.ID)
                ps.DoScore();
    }
}
