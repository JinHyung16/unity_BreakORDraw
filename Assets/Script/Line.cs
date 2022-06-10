using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // ���� ��� ���� ������ �� ������ �������� ȣ��
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
