using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private PlayerManager pM;
    public void Input(InputAction.CallbackContext value)
    {
        pM.Input(value.ReadValue<float>(), id);
    }
}
