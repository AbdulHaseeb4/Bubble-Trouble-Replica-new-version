using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;
    public Rigidbody2D rb;

    private float inputDirection = 0f;
    private float buttonDirection = 0f;

    void Update()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        float finalDirection = inputDirection + buttonDirection;
        finalDirection = Mathf.Clamp(finalDirection, -1f, 1f);

        rb.MovePosition(rb.position + new Vector2(finalDirection * speed * Time.fixedDeltaTime, 0f));
    }

    public void SetMoveDirection(float dir)
    {
        buttonDirection = dir;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ball"))
        {
            Debug.Log("GAME OVER!");
            BallTracker.instance.SetGameOver();
            FindObjectOfType<UIController>().ShowGameOver();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
