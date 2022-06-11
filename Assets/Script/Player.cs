using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    [SerializeField] private LayerMask touchLayer;
    [SerializeField] private LayerMask lineLayer;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        Ray ray = new Ray(transform.position, new Vector2(2f,-1f));
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

        if (Physics2D.Raycast(transform.position, new Vector2(2f, -1f), 10.0f, lineLayer))
        {
            Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector3.up * 0.05f, ForceMode2D.Impulse);
        }
    }
}
