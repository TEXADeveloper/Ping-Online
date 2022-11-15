using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 3;
    [SerializeField,Range(.00001f, .3f)] private float speedIncrement = 0f;
    [SerializeField, Range(.001f, .5f)] private float rayLength;
    [SerializeField] private Transform[] vertexes;
    [SerializeField] private LayerMask objectMask;
    private List<PlayerStats> lastHit = new List<PlayerStats>();
    private Rigidbody rb;
    private Vector3 direction;
    private float speed;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Timer.Throw += throwBall;
        Goal.ScoreInGoal += score;
    }

    private void throwBall()
    {
        lastHit.Clear();
        speed = initialSpeed;
        float x = Random.Range(0.3f, 1f) * (Random.Range(0,2) == 1? 1 : -1);
        float z = Random.Range(0.3f, 1f) * (Random.Range(0,2) == 1? 1 : -1);
        direction = new Vector3(x, 0, z);
        direction.Normalize();
    }

    void FixedUpdate()
    {
        throwRays();
        rb.velocity = direction * speed;
    }

    private void throwRays()
    {
        List<RaycastHit> vHits = new List<RaycastHit>();
        List<RaycastHit> hHits = new List<RaycastHit>();
        foreach (Transform vertex in vertexes)
        {
            foreach(RaycastHit hit in Physics.RaycastAll(vertex.position, Vector3.forward * (vertex.position.z > 0f? 1 : -1), rayLength, objectMask))
                vHits.Add(hit);
            foreach(RaycastHit hit in Physics.RaycastAll(vertex.position, Vector3.right * (vertex.position.x > 0f? 1 : -1), rayLength, objectMask))
                hHits.Add(hit);
        }
        int hMultiplier = (hHits.Count != 0)? -1 : 1;
        int vMultiplier = (vHits.Count != 0)? -1 : 1;
        direction = new Vector3(hMultiplier * direction.x, 0, vMultiplier * direction.z);
        savePlayer(vHits);
        savePlayer(hHits);
    }

    private void savePlayer(List<RaycastHit> hits)
    {
        foreach (RaycastHit hit in hits)
            if ((lastHit.Count == 0 || lastHit[lastHit.Count-1].transform != hit.transform) && hit.transform.tag.Equals("Player"))
            {
                speed += speedIncrement;
                lastHit.Add(hit.transform.GetComponent<PlayerStats>());
            }
    }

    private void score(Goal goal, bool twoPlayers)
    {
        transform.position = new Vector3(0f, transform.position.y, 0f);
        direction = Vector3.zero;

        int i = lastHit.Count - 1;
        while (i >= 0 && !twoPlayers)
            if (goal.GoalID != lastHit[i].ID)
            {
                goal.ScoreTo(lastHit[i]/*.DoScore()*/);
                return;
            }
        goal.ScoreTo(null);
    }

    void OnDisable()
    {
        Timer.Throw -= throwBall;
        Goal.ScoreInGoal -= score;
    }

    void OnDrawGizmosSelected()
    {
        foreach (Transform vertex in vertexes)
        {
            Gizmos.DrawLine(vertex.position, vertex.position + Vector3.forward * (vertex.position.z > 0f? 1 : -1) * rayLength);
            Gizmos.DrawLine(vertex.position, vertex.position + Vector3.right * (vertex.position.x > 0f? 1 : -1) * rayLength);
        }
    }
}
