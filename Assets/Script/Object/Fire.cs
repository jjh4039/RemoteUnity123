using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public CapsuleCollider2D colider;
    public Animator Animator;
    private bool isOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colider = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnManager.instance.Kill(collision);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            StartCoroutine(FireOn());
        }
    }
    IEnumerator FireOn()
    {
        Animator.SetBool("Fire",true);
        colider.enabled = true;
        isOn = true;
        yield return new WaitForSecondsRealtime(2);
        Animator.SetBool("Fire", false);
        colider.enabled = false;
        yield return new WaitForSecondsRealtime(4);
        isOn = false;
    }
}
