using UnityEngine;

public class eyefollowRED: MonoBehaviour
{ 
    public float maxDistance = 0.07f; 

    private Transform player;
    private Vector3 startLocalPos;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("PlayerRed");
        if (p != null)
            player = p.transform;

        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (player == null) return;
        
        Vector2 dir = (player.position - transform.parent.position).normalized;
        
        transform.localPosition = startLocalPos + (Vector3)dir * maxDistance;
    }
}