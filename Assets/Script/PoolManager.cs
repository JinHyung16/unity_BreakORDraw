using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region SingleTon
    private static PoolManager instance;

    public static PoolManager Instance
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
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            InitArray();
            Pooling();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public GameObject brickPrefab;
    public GameObject flagPrefab;
    public GameObject linePrefab;

    private GameObject[] bricks;
    private GameObject[] flags;
    private GameObject[] lines;

    private GameObject[] targets;

    private void InitArray()
    {
        bricks = new GameObject[100];
        flags = new GameObject[5];
        lines = new GameObject[50];
    }

    private void Pooling()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i] = Instantiate(brickPrefab);
            bricks[i].name = "Brick";
            bricks[i].SetActive(false);

            DontDestroyOnLoad(bricks[i]);
        }
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = Instantiate(flagPrefab);
            flags[i].name = "Flag";
            flags[i].SetActive(false);

            DontDestroyOnLoad(flags[i]);
        }
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i] = Instantiate(linePrefab);
            lines[i].name = "Line";
            lines[i].SetActive(false);

            DontDestroyOnLoad (lines[i]);
        }
    }

    public GameObject MakeObj(string name)
    {
        switch(name)
        {
            case "brick":
                targets = bricks;
                break;
            case "flag":
                targets = flags;
                break;
            case "line":
                targets = lines;
                break;
        }

        for (int i = 0; i < targets.Length; i++)
        {
            if(!targets[i].activeSelf)
            {
                targets[i].SetActive(true);
                return targets[i];
            }
        }

        return null;
    }
}
