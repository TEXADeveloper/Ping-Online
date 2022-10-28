using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField, Range(.001f, .5f)] private float rayLength;
    [SerializeField] private LayerMask objectMask;
    private Rigidbody rb;
    private PlayerController lastHit;
    private Vector3 direction;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        direction = new Vector3(0.7f, 0, 0.3f);
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
        for (int i = -1 ; i <= 1 ; i = i +2)
            foreach(RaycastHit hit in Physics.RaycastAll(transform.position, new Vector3(0, 0, i), rayLength, objectMask))
                vHits.Add(hit);
        List<RaycastHit> hHits = new List<RaycastHit>();
        for (int i = -1 ; i <= 1 ; i = i +2)
            foreach(RaycastHit hit in Physics.RaycastAll(transform.position, new Vector3(i, 0, 0), rayLength, objectMask))
                hHits.Add(hit);    
        int hMultiplier = (hHits.Count != 0)? -1 : 1;
        int vMultiplier = (vHits.Count != 0)? -1 : 1;
        direction = new Vector3(hMultiplier * direction.x, 0, vMultiplier * direction.z);
        savePlayer(vHits);
        savePlayer(hHits);
    }

    private void savePlayer(List<RaycastHit> hits)
    {
        foreach (RaycastHit hit in hits)
            if ((lastHit == null || lastHit.transform != hit.transform) || hit.transform.tag.Equals("Player"))
            {
                lastHit = hit.transform.GetComponent<PlayerController>();
                Debug.Log(lastHit);
            }
    }
}
