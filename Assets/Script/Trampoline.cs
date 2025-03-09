using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Trampoline : MonoBehaviour
{
    public BoxCollider2D colider;
    public Animator Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().linearVelocityY = 10;
            Animator.SetTrigger("Jump");

        }
    }
}
