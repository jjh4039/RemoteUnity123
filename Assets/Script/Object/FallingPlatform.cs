using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class FallingPlatform : MonoBehaviour
{
    public BoxCollider2D colider;
    public Animator Animator;
    public Rigidbody2D rb;
    public float gravityScale = 0.5f;
    private Vector3 start_pos;
    private GameObject child;
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        start_pos = transform.position;
        colider = child.GetComponent<BoxCollider2D>();
        Animator = child.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && collision.transform.position.y > transform.position.y + 0.5f)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Animator.SetBool("Fall", true);
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSecondsRealtime(1);
        child.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        Reset();
        child.SetActive(true);

    }
    private void Reset()
    {
        transform.position = start_pos;

        rb.bodyType = RigidbodyType2D.Static;
        Animator.SetBool("Fall", false);
    }
}

