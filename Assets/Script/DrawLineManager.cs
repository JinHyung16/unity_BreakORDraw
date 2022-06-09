using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    
    private List<Vector2> fingerPositions = new List<Vector2>();

    private int degree = 0;

    public Transform linePoint;
    /**
     * Temp : Revomed gravity part
     * **/

    //[SerializeField] private GameObject gravityLinePrefab;
    //private GameObject gravityLine;
    //private LineRenderer gravityLineRenderer;
    //private EdgeCollider2D gravityEdgeCollider;

    private void Update()
    {
        InputMouse();

        /*
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(currentLine);
            gravityLine.SetActive(true);
        }
        */
    }

    private void InputMouse()
    {
        // left mouse click

        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 3f)
            {
                if(fingerPositions.Count == 2)
                {
                    Debug.Log(Vector2.Angle(fingerPositions[0], tempFingerPos));
                    // Measure the angle between the first and second points
                    if (Vector2.Angle(fingerPositions[0], tempFingerPos) < 45.0f)
                    {
                        degree = 0;
                    }
                    /*
                    else if (Vector2.Angle(fingerPositions[0], tempFingerPos) >= 30.0f && Vector2.Angle(fingerPositions[0], tempFingerPos) < 60.0f)
                    {
                        degree = 45;
                    }
                    */
                    else
                    {
                        degree = 90;
                    }
                }
                UpdateLine(tempFingerPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentLine.transform.SetParent(linePoint);
        }
    }

    private void CreateLine()
    {
        currentLine = PoolManager.Instance.MakeObj("line");
        currentLine.transform.position = Vector3.zero;
        currentLine.transform.rotation = Quaternion.identity;

        /*
         * gravityLine = Instantiate(gravityLinePrefab, Vector3.zero, Quaternion.identity);
         * gravityLine.SetActive(false);
         * gravityLineRenderer = gravityLine.GetComponent<LineRenderer>();
         * gravityEdgeCollider = gravityLine.GetComponent<EdgeCollider2D>();
         * gravityLineRenderer.SetPosition(0, fingerPositions[0]);
         * gravityLineRenderer.SetPosition(1, fingerPositions[1]);
         * gravityEdgeCollider.points = fingerPositions.ToArray();
         *
         */

        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        edgeCollider.Reset();
        fingerPositions.Clear();

        // if dot -> draw line do[two]
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);

        edgeCollider.points = fingerPositions.ToArray();
    }

    private void UpdateLine(Vector2 newFingerPos)
    {

        /**
         * gravityLineRenderer.positionCount++;
         * gravityLineRenderer.SetPosition(gravityLineRenderer.positionCount - 1, newFingerPos);
         * gravityEdgeCollider.points = fingerPositions.ToArray();
         * **/
        Vector2 position = newFingerPos;
        switch (degree)
        {
            case 0:
                {
                    position = new Vector2(newFingerPos.x, fingerPositions[0].y);
                }
                break;
                /*
            case 45:
                {
                    position = new Vector2(newFingerPos.x, Mathf.Tan(45f) * newFingerPos.x);
                }
                break;
                */
            case 90:
                {
                    position = new Vector2(fingerPositions[0].x, newFingerPos.y);
                }
                break;
        }
        fingerPositions.Add(position);
        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);

        edgeCollider.points = fingerPositions.ToArray();
    }

}
