using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ParticleSystem breakParticle;

    [SerializeField] private float disableTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            if (collision.gameObject.name == "Left")
            {
                this.gameObject.transform.SetParent(null);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void BrickController()
    {
        breakParticle.Play();
        StartCoroutine(DisableObject());
    }

    IEnumerator DisableObject()
    {
        yield return Cashing.YieldInstruction.WaitForSeconds(disableTime);
        this.gameObject.SetActive(false);
        this.gameObject.transform.SetParent(null);
    }
}
