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
                GameManager.Instance.isDraw = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Right")
            {
                this.gameObject.transform.SetParent(null);
                this.gameObject.SetActive(false);
            }
        }
    }
}
