using UnityEngine;
using System;

public class Goal : MonoBehaviour
{
    public static event Action Score;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Ball"))
            Score?.Invoke();
    }
}
