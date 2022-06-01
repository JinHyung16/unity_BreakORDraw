using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    #region SingleTon
    private static DrawLineManager instance;
    public static DrawLineManager Instance
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


    [SerializeField] private GameObject linePrefab;
    
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    
    private List<Vector2> fingerPositions = new List<Vector2>();

    /**
     * Temp : Revomed gravity part
     * **/

    //[SerializeField] private GameObject gravityLinePrefab;
    //private GameObject gravityLine;
    //private LineRenderer gravityLineRenderer;
    //private EdgeCollider2D gravityEdgeCollider;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CreateLine();

        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                UpdateLine(tempFingerPos);
        }
        
        /*
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(currentLine);
            gravityLine.SetActive(true);
        }
        */
    }

    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
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
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
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
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        edgeCollider.points = fingerPositions.ToArray();
    }

}
