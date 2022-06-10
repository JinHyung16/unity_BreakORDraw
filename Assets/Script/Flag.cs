using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Right")
            {
                int index = Random.Range(1, 4);
                if (!GameManager.Instance.isOver)
                {
                    MapManager.Instance.DrawStage(index);
                    Debug.Log(index);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            if(collision.gameObject.name == "Right")
            {
                //this.gameObject.SetActive(false);
            }
        }
    }
}
