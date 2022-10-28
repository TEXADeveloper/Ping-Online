using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int id;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Ball"))
            doScore();
    }

    private void doScore()
    {
        Debug.Log("score " + id);
    }
}
