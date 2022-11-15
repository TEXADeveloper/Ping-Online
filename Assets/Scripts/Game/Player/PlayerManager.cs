using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> freeSpawnPoints = new List<Transform>();
    private List<Transform> ocupatedSpawnPoints = new List<Transform>();

    public PlayerStats[] GetPlayersStats()
    {
        return GetComponentsInChildren<PlayerStats>();
    }

    public Transform GetEmptyTransform()
    {
        if (freeSpawnPoints.Count <= 0)
            return null;
        ocupatedSpawnPoints.Add(freeSpawnPoints[0]);
        freeSpawnPoints.Remove(freeSpawnPoints[0]);
        return ocupatedSpawnPoints[ocupatedSpawnPoints.Count -1];      
    }
}
