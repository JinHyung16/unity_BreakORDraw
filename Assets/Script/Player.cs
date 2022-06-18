using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid2d;
    Animator anim;

    [SerializeField] private LayerMask touchLayer;
    [SerializeField] private LayerMask lineLayer;

    private void Start()
    {
        rigid2d = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        BreakSystem();
        RayPlayerLine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Down" || collision.gameObject.name == "Left")
            {
                anim.SetTrigger("isDie");
                GameManager.Instance.GameOver();
            }
        }
        if(collision.CompareTag("Brick"))
        {
            anim.SetTrigger("isDie");
            GameManager.Instance.GameOver();
        }
    }

    private void BreakSystem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10.0f, touchLayer);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Brick"))
                {
                    Debug.Log("Brick Touch");
                    hit.collider.gameObject.GetComponent<Brick>().BrickController();
                }
            }

            anim.SetTrigger("isAttack");
        }
    }

    private void RayPlayerLine()
    {
        Debug.DrawRay(new Vector2(this.transform.position.x + 2.0f, this.transform.position.y), Vector2.down * 10.0f, Color.red, 5f);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x + 1.0f, this.transform.position.y), Vector2.down, 10.0f, lineLayer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Line"))
            {
                Debug.Log("line Ãæµ¹");
                rigid2d.AddForce(Vector3.up * 1.3f, ForceMode2D.Force);
            }
        }
    }
}
