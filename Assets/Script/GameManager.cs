using UnityEngine;
using UnityEngine.UI;

sealed class GameManager : MonoBehaviour
{
    #region SingleTon
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public int stageIndex = 1;

    [SerializeField] private DrawLineManager drawLineManager; 

    // about UI
    public GameObject startPanel;
    public GameObject resultPanel;

    public Button startBt;
    public Button restartBt;

    private void Start()
    {
        startPanel.SetActive(true);
        resultPanel.SetActive(false);

        startBt.onClick.AddListener(GameStart);
        restartBt.onClick.AddListener(ReGame);

        Time.timeScale = 0; // pause game
    }

    private void GameStart()
    {
        startPanel.SetActive(false);
        drawLineManager.gameObject.SetActive(true);
        
        for (int i = 1; i <= 4; i++)
        {
            MapManager.Instance.DrawStage(i);
        }
        
        Time.timeScale = 1; // play game
    }

    private void ReGame()
    {
        resultPanel.SetActive(false);

        Time.timeScale = 1; // play game
    }

    public void GameOver()
    {
        resultPanel.SetActive(true);
        drawLineManager.gameObject.SetActive(false);
        MapManager.Instance.ResetMap();

        Time.timeScale = 0; // pause game
    }
}
