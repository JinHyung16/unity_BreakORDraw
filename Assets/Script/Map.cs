using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
    public int stage;
    public int brick;
    public int block;

    public Map(Map m)
    {
        stage = m.stage;
        brick = m.brick;
        block = m.block;
    }
}
