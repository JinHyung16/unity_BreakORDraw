using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private GameObject gridMap;
    [SerializeField] private GameObject flagMap;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject clouds;
    [SerializeField] private GameObject itemPoint;
    [SerializeField] private GameObject linePoint;

    [Range(0, 15)] [SerializeField] private float scollSpeed = 0.8f;

    private void Start()
    {
        gridMap.transform.position = Vector2.zero;
        background.transform.position = Vector2.zero;
    }
    private void Update()
    {
        MapMove();
        BackGroundMove();

        ResetMap();
    }

    private void LateUpdate()
    {
        CloudsMove();
    }

    private void MapMove()
    {
        gridMap.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
        flagMap.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
        itemPoint.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
        linePoint.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
    }
    private void BackGroundMove()
    {
        background.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
        if (background.transform.position.x <= -11.0f)
        {
            background.transform.position = Vector2.zero;
        }
    }
    private void CloudsMove()
    {
        clouds.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
        if(clouds.transform.position.x <= -11.0f)
        {
            clouds.transform.position = Vector2.zero;
        }
    }

    private void ResetMap()
    {
        if (GameManager.Instance.isOver)
        {
            gridMap.transform.position = Vector2.zero;
            background.transform.position = Vector2.zero;
            clouds.transform.position = Vector2.zero;
        }
    }
}