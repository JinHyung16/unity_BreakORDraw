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

    [Tooltip("ground tile map in grid")] public Tilemap groundMap;
    [Tooltip("flag tile map in grid")] public Tilemap flagMap;

    [Tooltip("tile base set in tile map")] public TileBase groundTile;
    [Tooltip("tile base set in tile map")] public TileBase flagTile;

    [SerializeField] private List<Map> stageOne;
    [SerializeField] private List<Map> stageTwo;
    [SerializeField] private List<Map> stageThree;
    [SerializeField] private List<Map> stageFour;

    public Map blankMap;

    [SerializeField] private Vector3Int tilePoint;

    public Transform itemPoint;

    private void Start()
    {
        LoadMap();

        groundMap = groundMap.GetComponent<Tilemap>();
        flagMap = flagMap.GetComponent<Tilemap>();

        tilePoint = new Vector3Int(-8, -1, 0);
    }
    private void LoadMap()
    {
        // clear data base
        stageOne.Clear();
        stageTwo.Clear();

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
            stage = stage,
            brick = brick,
            block = block,
            flag = flag,
        };

        switch (map.stage)
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
            if (stageOne[i].block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, -1, tilePoint.z)), null);
            }

            if(stageOne[i].flag == 1)
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), flagTile);
            }
            else
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), null);
            }

            if (stageOne[i].brick == 1)
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
            if (stageTwo[i].block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), null);
            }

            if (stageTwo[i].flag == 1)
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), flagTile);
            }
            else
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), null);
            }

            if (stageTwo[i].brick == 1)
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
            if (stageThree[i].block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), null);
            }

            if (stageThree[i].flag == 1)
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), flagTile);
            }
            else
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), null);
            }

            if (stageThree[i].brick == 1)
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
            if (stageFour[i].block == 1)
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                groundMap.SetTile(groundMap.WorldToCell(tilePoint), null);
            }

            if (stageFour[i].flag == 1)
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), flagTile);
            }
            else
            {
                flagMap.SetTile(groundMap.WorldToCell(new Vector3Int(tilePoint.x, 0, tilePoint.z)), null);
            }

            if (stageFour[i].brick == 1)
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
        tilePoint = new Vector3Int(-10, 0, 0); // tilePoint setting
        LoadMap();
    }
}
