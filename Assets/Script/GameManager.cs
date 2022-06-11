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

    public DrawLineManager drawLineManager;
    public GameObject player;

    public bool isOver = false;

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

        isOver = false;
        MapManager.Instance.DrawStage(0); // draw stage one

        Time.timeScale = 0; // pause game
    }

    private void GameStart()
    {
        startPanel.SetActive(false);

        drawLineManager.gameObject.SetActive(true);

        isOver = false;

        Time.timeScale = 1; // play game
    }

    private void ReGame()
    {
        resultPanel.SetActive(false);
        drawLineManager.gameObject.SetActive(true);
        player.SetActive(true);
        player.transform.position = new Vector2(-7.18f, 1.8f);
        player.transform.rotation = Quaternion.identity;
        isOver = false;

        Time.timeScale = 1; // play game
    }

    public void GameOver()
    {
        resultPanel.SetActive(true);

        drawLineManager.gameObject.SetActive(false);
        player.SetActive(false);

        isOver = true;
        MapManager.Instance.ResetMap();

        Time.timeScale = 0; // pause game
    }
}
