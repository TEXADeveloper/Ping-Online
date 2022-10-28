using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerController[] players;

    void Start()
    {
        players = this.transform.GetComponentsInChildren<PlayerController>();
    }

    public void Input(float input, int id)
    {
        foreach (PlayerController player in players)
            if (player.ID == id)
                    player.ReceiveInput(input);
    }
}
