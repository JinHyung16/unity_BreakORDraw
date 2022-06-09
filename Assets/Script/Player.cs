using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    [SerializeField] private LayerMask touchLayer;
    private bool isAttack = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        BreakSystem();
        AnimationController();
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
                    isAttack = true;
                }
            }
        }
    }

    private void AnimationController()
    {
        if(isAttack)
        {
            anim.SetBool("isAttack", true);
            isAttack = false;
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}
