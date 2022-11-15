using UnityEngine;
using TMPro;


public class InputField : MonoBehaviour
{
    [SerializeField] private MenuController mC;
    private TMP_InputField iF;
    private bool selected = false;

    void Start()
    {
        iF = this.GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (selected && Input.GetKeyDown(KeyCode.Return))
            mC.JoinGame();
    }

    public void SetSelected(bool value)
    {
        selected = value;
    }
}
