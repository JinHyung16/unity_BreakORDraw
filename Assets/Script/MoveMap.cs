using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public GameObject gridMap;
    public GameObject background;
    public GameObject clouds;
    public GameObject itemPoint;
    public GameObject linePoint;

    [Range(0, 15)] [SerializeField] private float scollSpeed = 0.8f;

    private void Start()
    {
        gridMap.transform.position = Vector2.zero;
        background.transform.position = Vector2.zero;
    }
    private void Update()
    {
        GridMapMove();
        BackGroundMove();

        ResetMap();
    }

    private void LateUpdate()
    {
        CloudsMove();
    }

    private void GridMapMove()
    {
        gridMap.transform.Translate(Vector2.left * scollSpeed * Time.deltaTime);
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