using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Down")
            {
                anim.SetTrigger("isDie");
                GameManager.Instance.GameOver();
            }
        }
    }
}
