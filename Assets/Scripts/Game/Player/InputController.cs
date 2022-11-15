using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class InputController : NetworkBehaviour
{
    private Player p;
    private PlayerStats pS;
    private NetworkIdentity nI;
    private bool movementAvailable;
    [SerializeField] private Material mat;

    void Start()
    {
        GameManager.GameState += canMove;
        setReferences();
        if (isLocalPlayer)
            setPlayerColor();
    }

    private void setReferences()
    {
        p = GetComponent<Player>();
        pS = GetComponent<PlayerStats>();
        nI = GetComponent<NetworkIdentity>();

        pS.SetID((int) nI.netId);
        transform.parent.GetComponent<GoalReference>().Goal.GoalID = (int) nI.netId;
    }

    private void setPlayerColor()
    {
        Color playerColor = Random.ColorHSV(.16f, .5f, .4f, .5f, 1f, 1f);
        Material newMat = new Material(mat);
        newMat.color = playerColor;
        GetComponent<Renderer>().material = newMat;
    }

    private void canMove(bool value)
    {
        movementAvailable = value;
        if (!value)
        {
            p.ReceiveInput(0);
            this.transform.position = transform.parent.position;
        }
    }

    public void Input(InputAction.CallbackContext value)
    {
        if (movementAvailable && isLocalPlayer)
            p.ReceiveInput(value.ReadValue<float>());
    }

    void OnDisable()
    {
        GameManager.GameState -= canMove;
    }
}
