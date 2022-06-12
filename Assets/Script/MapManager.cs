using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

sealed class MapManager : MonoBehaviour
{
    #region SingleTon
    private static MapManager instance;
    public static MapManager Instance
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

    [SerializeField] private MoveMap moveMap;

    [Tooltip("ground tile map in grid")] public Tilemap groundMap;

    [Tooltip("tile base set in tile map")] public TileBase groundTile;

    [SerializeField] private List<Map> stageOne = new List<Map>();
    [SerializeField] private List<Map> stageTwo = new List<Map>();
    [SerializeField] private List<Map> stageThree = new List<Map>();
    [SerializeField] private List<Map> stageFour = new List<Map>();

    public Map blankMap;

    [SerializeField] private Vector3Int tilePoint;

    public Transform itemPoint;

    private void Start()
    {
        LoadMap();
        groundMap = groundMap.GetComponent<Tilemap>();

        tilePoint = new Vector3Int(-8, -1, 0);
    }
    private void LoadMap()
    {
        // clear data base
        stageOne.Clear();
        stageTwo.Clear();
        stageThree.Clear();
        stageFour.Clear();

        // read csv file
        List<Dictionary<string, object>> data = CSVReader.Read("MapData");

        for (var i = 0; i < data.Count; i++)
        {
            int stage = int.Parse(data[i]["Stage"].ToString(), System.Globalization.NumberStyles.Integer);
            int brick = int.Parse(data[i]["Brick"].ToString(), System.Globalization.NumberStyles.Integer);
            int block = int.Parse(data[i]["Block"].ToString(), System.Globalization.NumberStyles.Integer);
            int flag = int.Parse(data[i]["Flag"].ToString(), System.Globalization.NumberStyles.Integer);
            AddMap(stage, brick, block, flag);
        }
    }

    private void AddMap(int stage, int brick, int block, int flag)
    {
        Map map = new Map(blankMap)
        {
            Stage = stage,
            Brick = brick,
            Block = block,
            Flag = flag,
        };

        switch (map.Stage)
        {
            case 1:
                stageOne.Add(map);
                break;
            case 2:
                stageTwo.Add(map);
                break;
            case 3:
                stageThree.Add(map);
                break;
            case 4:
                stageFour.Add(map);
                break;
        }
    }

    public void DrawStage(int stageNum)
    {
        switch (stageNum)
        {
            case 0:
                DrawStageOne();
                break;
            case 1:
                DrawStageTwo();
                break;
            case 2:
                DrawStageThree();
                break;
            case 3:
                DrawStageFour();
                break;
        }
    }

    private void DrawStageOne()
    {
        for (int i = 0; i < stageOne.Count; i++)
        {
            if (stageOne[i].Block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), null);
            }

            if(stageOne[i].Flag == 1)
            {
                GameObject flag = PoolManager.Instance.MakeObj("flag");
                flag.transform.SetParent(itemPoint);
                flag.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                flag.SetActive(true);
            }

            if (stageOne[i].Brick == 1)
            {
                GameObject brick = PoolManager.Instance.MakeObj("brick");
                brick.transform.SetParent(itemPoint);
                brick.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                brick.SetActive(true);
            }

            tilePoint.x += 1;
        }
    }

    private void DrawStageTwo()
    {
        tilePoint = new Vector3Int(15, -1, 0);

        for (int i = 0; i < stageTwo.Count; i++)
        {
            if (stageTwo[i].Block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), null);
            }

            if (stageTwo[i].Flag == 1)
            {
                GameObject flag = PoolManager.Instance.MakeObj("flag");
                flag.transform.SetParent(itemPoint);
                flag.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                flag.SetActive(true);
            }

            if (stageTwo[i].Brick == 1)
            {
                GameObject brick = PoolManager.Instance.MakeObj("brick");
                brick.transform.SetParent(itemPoint);
                brick.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                brick.SetActive(true);
            }


            tilePoint.x += 1;
        }
    }

    private void DrawStageThree()
    {
        tilePoint = new Vector3Int(15, -1, 0);
        for (int i = 0; i < stageThree.Count; i++)
        {
            if (stageThree[i].Block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), null);
            }

            if (stageThree[i].Flag == 1)
            {
                GameObject flag = PoolManager.Instance.MakeObj("flag");
                flag.transform.SetParent(itemPoint);
                flag.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                flag.SetActive(true);
            }

            if (stageThree[i].Brick == 1)
            {
                GameObject brick = PoolManager.Instance.MakeObj("brick");
                brick.transform.SetParent(itemPoint);
                brick.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                brick.SetActive(true);
            }

            tilePoint.x += 1;
        }
    }
    private void DrawStageFour()
    {
        tilePoint = new Vector3Int(15, -1, 0);
        for (int i = 0; i < stageFour.Count; i++)
        {
            if (stageFour[i].Block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), null);
            }

            if (stageFour[i].Flag == 1)
            {
                GameObject flag = PoolManager.Instance.MakeObj("flag");
                flag.transform.SetParent(itemPoint);
                flag.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                flag.SetActive(true);
            }

            if (stageFour[i].Brick == 1)
            {
                GameObject brick = PoolManager.Instance.MakeObj("brick");
                brick.transform.SetParent(itemPoint);
                brick.transform.position = new Vector3(tilePoint.x, 0.4f, tilePoint.z);
                brick.SetActive(true);
            }

            tilePoint.x += 1;
        }
    }

    public void ResetMap()
    {
        itemPoint.DetachChildren();
        moveMap.ResetMap();
        tilePoint = new Vector3Int(-10, 0, 0); // tilePoint setting
        LoadMap();
    }
}
