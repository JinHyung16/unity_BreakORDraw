using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // 선이 모두 왼쪽 보더와 다 만나서 지나가면 호출
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Border"))
        {
            if(collision.gameObject.name == "Left")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
