using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    [Header("Component")]
    public FruitManager fruitManager;
    public Player player;
    public IntroduceTextManager introduceTextManager;
    public Bar bar;
    public BarUi barUi;
    public MainCamera mainCamera;
    public CutScene cutScene;
    public GameOver gameOver;
    public SceneStep sceneStep;

    void Awake()
    {
        Instance = this;
    }
}
