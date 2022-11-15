using UnityEngine;
using TMPro;

public class TemplateReferences : MonoBehaviour
{
    [HideInInspector] public TMP_Text ID;
    [HideInInspector] public TMP_Text Score;
    [HideInInspector] public TMP_Text Wins;

    void Awake()
    {
        foreach(TMP_Text text in this.GetComponentsInChildren<TMP_Text>())
            switch (text.transform.name)
            {
                case "ID":
                    ID = text;
                    break;
                case "Score":
                    Score = text;
                    break;
                case "Wins":
                    Wins = text;
                    break;
                default:
                    Debug.LogError("Something went wrong");
                    break;
            }
    }
}
