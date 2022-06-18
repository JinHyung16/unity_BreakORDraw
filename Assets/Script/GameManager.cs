using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

    AudioSource audio;

    [SerializeField] private float curTime = 0.0f;
    [SerializeField] private float startTime = 0.0f;
    public AudioClip startBGM;

    [SerializeField] private DrawLineManager drawLineManager;

    public GameObject player;

    [Range(0, 1.0f)] [SerializeField] private float overTime = 0.5f;
    public bool isDraw = false;

    // about UI
    public GameObject startPanel;
    public GameObject resultPanel;

    public Button startBt;
    public Button restartBt;

    public Text timerText;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        BGM_Play("start");
        startPanel.SetActive(true);
        resultPanel.SetActive(false);

        startBt.onClick.AddListener(GameStart);
        restartBt.onClick.AddListener(ReGame);

        MapManager.Instance.DrawStage(0); // draw stage one

        startTime = Time.time;

        Time.timeScale = 0; // pause game
    }

    private void Update()
    {
        if(isDraw)
        {
            DrawMap();
        }

        Timer();
    }

    private void DrawMap()
    {
        int index = Random.Range(1, 4);
        MapManager.Instance.DrawStage(index);
        Debug.Log(index);
        isDraw = false;
    }
    private void Timer()
    {
        curTime += (Time.deltaTime - startTime);
        timerText.text = curTime.ToString("F2");
    }

    private void BGM_Play(string name)
    {
        switch(name)
        {
            case "start":
                audio.clip = startBGM;
                break;
            default:
                audio.clip = null;
                break;
        }

        audio.Play();
    }

    private void GameStart()
    {
        startPanel.SetActive(false);

        drawLineManager.gameObject.SetActive(true);

        player.SetActive(true);
        player.transform.position = new Vector2(-7.18f, 1.8f);
        player.transform.rotation = Quaternion.identity;

        BGM_Play("none");

        curTime = 0.0f;

        Time.timeScale = 1; // play game
    }

    private void ReGame()
    {
        SceneManager.LoadScene(0); // reload scene

        resultPanel.SetActive(false);

        drawLineManager.gameObject.SetActive(true);

        player.SetActive(true);
        player.transform.position = new Vector2(-7.18f, 1.8f);
        player.transform.rotation = Quaternion.identity;

        MapManager.Instance.DrawStage(0); // draw stage one

        curTime = 0.0f;

        Time.timeScale = 1; // play game
    }

    public void GameOver()
    {
        StartCoroutine(Over());
    }

    IEnumerator Over()
    {
        yield return Cashing.YieldInstruction.WaitForSeconds(overTime);

        resultPanel.SetActive(true);
        drawLineManager.gameObject.SetActive(false);
        player.SetActive(false);

        MapManager.Instance.ResetMap();
        PoolManager.Instance.ResetObject();

        curTime = 0.0f;

        Time.timeScale = 0; // pause game
    }
}
