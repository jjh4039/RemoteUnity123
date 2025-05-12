using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static public SpawnManager instance;

    [Header("Component")]
    public GameObject player;
    private GameObject LivingPlayer;

    [Header("Field")]
    public int deathCount;
    public Vector3 spawnPoint;

    void Start()
    {
        deathCount = 0;
        instance = this;
        spawnPoint = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if(!GameManager.Instance.player.isLive)
        {
            LivingPlayer = GameObject.FindGameObjectWithTag("Player");

            if (Input.GetKeyDown(KeyCode.R))
            {
                Rebirth();
            }
        }


    }

    public void Rebirth()
    {
        player.gameObject.SetActive(true);
        player.transform.position = spawnPoint;
        GameManager.Instance.gameOver.gameOverUi.SetActive(false);

        if (deathCount == 1)
        {
            GameManager.Instance.introduceTextManager.StartCoroutine("FirstDie");
        }
    }

    public void Kill()
    {
        if (!GameManager.Instance.player.isLive)
        {
            Destroy(LivingPlayer.gameObject);
            GameManager.Instance.player = null;
            GameManager.Instance.player.isLive = true;
        }
    }

    public void Kill(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!GameManager.Instance.player.isLive)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.player = null;
                GameManager.Instance.player.isLive = true;
            }
        } 
    }
}
