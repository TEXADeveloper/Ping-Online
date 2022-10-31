using UnityEngine;
using System;

public class Goal : MonoBehaviour
{
    [SerializeField] private Ball ballScript; 
    public int goalID = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Ball"))
            ballScript?.Score(goalID);
    }
}
