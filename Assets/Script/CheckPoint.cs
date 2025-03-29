using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [HideInInspector] public bool checkPointReached = false;
    [HideInInspector] public BoxCollider2D Colider;
    [HideInInspector] public GameObject Check;
    [HideInInspector] public Animator animator;
    public Text spawnText;
    public CanvasGroup textAlpha;

    void Start()
    {
        Colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        Check = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnManager.instance.spawnPoint == transform.position)
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
            StartCoroutine(TextFlow());
            SpawnManager.instance.spawnPoint = transform.position;
        }
    }

    IEnumerator FlagOut()
    {
        animator.SetBool("Flag Out", true);  
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Flag Out", false);
    }

    IEnumerator TextFlow()
    {
        spawnText.transform.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y + 0.2f, 0);
        textAlpha.alpha = 1;
        for (int i = 0; i < 100; i++)
        {
            spawnText.transform.position += new Vector3(0, 0.002f, 0);
            textAlpha.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

