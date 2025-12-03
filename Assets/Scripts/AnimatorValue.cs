using UnityEngine;

public class AnimatorValue : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        animator.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
