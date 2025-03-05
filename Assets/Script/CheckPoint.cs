using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool checkPointReached = false;
    public BoxCollider2D Colider;
    public GameObject Check;
    public GameObject gameobject;
    public Animator animator;

    void Start()
    {
        Colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        Check = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnManager.instance.SpawnPoint == transform.position)
        {
            checkPointReached = true;
        }
        else
        {
            checkPointReached = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !checkPointReached)
        {
            checkPointReached = true;
            StartCoroutine(FlagOut());
            SpawnManager.instance.SpawnPoint = transform.position;

        }
        
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointReached = false;

        }
    }
    */
    IEnumerator FlagOut()
    {
        animator.SetBool("Flag Idle", false);
        animator.SetBool("Flag Out", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Flag Out", false);
        animator.SetBool("Flag Idle", true);
    }
}

