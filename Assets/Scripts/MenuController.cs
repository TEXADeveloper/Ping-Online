using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Mirror;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button[] buttons;
    private int mode = -1;
    private string ip = "";

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void SelectMode(int value)
    {
        mode = value;
    }

    public void ChangeIP(string value)
    {
        ip = value;
    }

    public void HostGame()
    {
        if (mode >= 0 && mode <= 1)
        {
            int players = (mode + 1) * 2;
            NetworkManager.singleton.onlineScene = "Assets/Scenes/" + players.ToString() + "Player.unity";
            NetworkManager.singleton.maxConnections = players;
            NetworkManager.singleton.StartHost();
        } else
        {
            error("You should select a game mode to host.");
        }
    }

    public void JoinGame()
    {
        if (ip.Length > 0)
        {
            NetworkManager.singleton.networkAddress = ip;
            StartCoroutine(connectionError());
            NetworkManager.singleton.StartClient();            
        } else
        {
            error("You have to add an IP direction to connect.");
        }
    }

    private IEnumerator connectionError()
    {
        yield return new WaitForSeconds(.5f);
        error("Error: Connection timed out.");
    }

    private void error(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
        foreach (Button button in buttons)
                button.interactable = false;
    }
}
