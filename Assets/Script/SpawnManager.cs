using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static public SpawnManager instance;
    public GameObject player;
    public Vector3 SpawnPoint;
    public bool isDead = false;
    private GameObject LivingPlayer;
    void Start()
    {
        instance = this;
        SpawnPoint = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            LivingPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void Respawn()
    {
        if (isDead)
        {
            LivingPlayer = Instantiate(player, SpawnPoint, Quaternion.identity);
            LivingPlayer.name = "Player";
            Player p1 = LivingPlayer.GetComponent<Player>();
            GameManager.Instance.player = p1;
            p1.enabled = false;
            isDead = false;
        }
    }
    public void Kill()
    {
        if (!isDead)
        {
            Destroy(LivingPlayer.gameObject);
            GameManager.Instance.player = null;
            isDead = true;
        }
    }
}
