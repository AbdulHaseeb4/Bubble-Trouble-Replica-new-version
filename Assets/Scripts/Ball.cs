using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Vector2 startForce;
    public GameObject nextBall;
    public Rigidbody2D rb;

    void Start()
    {
        BallTracker.instance.RegisterBall();

        rb.AddForce(startForce, ForceMode2D.Impulse);
    }

    public void Split()
    {
        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4f, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4f, Quaternion.identity);

            ball1.GetComponent<Ball>().startForce = new Vector2(2f, 5f);
            ball2.GetComponent<Ball>().startForce = new Vector2(-2f, 5f);
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForEndOfFrame(); 
        BallTracker.instance.UnregisterBall();
        Destroy(gameObject);
    }
}
