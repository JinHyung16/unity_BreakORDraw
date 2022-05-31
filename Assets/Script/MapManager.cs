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

    [Tooltip("ground tile map in grid")] public Tilemap ground;
    [Tooltip("tile base set in tile map")] public TileBase groundTile;

    [SerializeField] private List<Map> stageOne;
    [SerializeField] private List<Map> stageTwo;
    [SerializeField] private List<Map> stageThree;
    [SerializeField] private List<Map> stageFour;

    public Map blankMap;

    [SerializeField] private Vector3Int tilePoint;

    private void Start()
    {
        LoadMap();

        ground = ground.GetComponent<Tilemap>();
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
            AddMap(stage, brick, block);
        }
    }

    private void AddMap(int stage, int brick, int block)
    {
        Map map = new Map(blankMap)
        {
            stage = stage,
            brick = brick,
            block = block,
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
            case 1:
                DrawStageOne();
                break;
            case 2:
                DrawStageTwo();
                break;
            case 3:
                DrawStageThree();
                break;
            case 4:
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
                ground.SetTile(ground.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                ground.SetTile(ground.WorldToCell(tilePoint), null);
            }

            tilePoint.x += 1;
        }
    }

    private void DrawStageTwo()
    {
        for (int i = 0; i < stageTwo.Count; i++)
        {
            if (stageTwo[i].block == 1)
            {
                ground.SetTile(ground.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                ground.SetTile(ground.WorldToCell(tilePoint), null);
            }

            tilePoint.x += 1;
        }
    }

    private void DrawStageThree()
    {
        for (int i = 0; i < stageThree.Count; i++)
        {
            if (stageThree[i].block == 1)
            {
                ground.SetTile(ground.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                ground.SetTile(ground.WorldToCell(tilePoint), null);
            }

            tilePoint.x += 1;
        }
    }
    private void DrawStageFour()
    {
        for (int i = 0; i < stageFour.Count; i++)
        {
            if (stageFour[i].block == 1)
            {
                ground.SetTile(ground.WorldToCell(tilePoint), groundTile);
            }
            else
            {
                ground.SetTile(ground.WorldToCell(tilePoint), null);
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
