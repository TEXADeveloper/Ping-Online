using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
using Mirror;
using TMPro;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text connectedText;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] public GameObject TimerPanel;
    [SerializeField] private GameObject resultsPanel;
    [SyncVar] private int playerAmount = 0;
    [SyncVar] private int maxPlayerAmount = 0;
    public static event Action<bool> GameState;
    private bool isStopped = true;

    [Server, ClientRpc]
    public void GameStatus(bool value)
    {
        GameState?.Invoke(value);
        connectionPanel.SetActive(!value);
        TimerPanel.SetActive(value);
        resultsPanel.SetActive(false);
    }

    [ClientRpc]
    public void EndGame()
    {
        GameState?.Invoke(false);
        connectionPanel.SetActive(false);
        TimerPanel.SetActive(false);
        resultsPanel.SetActive(true);
    }

    public void Update()
    {
        if (isServer)
        {
            playerAmount = NetworkServer.connections.Count;
            maxPlayerAmount = NetworkManager.singleton.maxConnections;
            if (playerAmount == maxPlayerAmount && !isStopped)
            {
                isStopped = false;
                startButton.interactable = true;
            } else if (isStopped)
            {
                isStopped = false;
                startButton.interactable = false;
                GameStatus(false);
            }
        }
        connectedText.text = playerAmount + "/" + maxPlayerAmount;
    }

    public void DisconnectButton()
    {
        if (isClient)
            NetworkManager.singleton.StopClient();
        if (isServer)
            NetworkManager.singleton.StopServer();
        SceneManager.LoadSceneAsync(0);
    }
}
