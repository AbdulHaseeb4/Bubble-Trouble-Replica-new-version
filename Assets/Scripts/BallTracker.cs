using UnityEngine;

public class BallTracker : MonoBehaviour
{
    public static BallTracker instance;

    private int ballCount = 0;
    private bool gameOver = false;
    private bool levelStarted = false;
    private bool levelCompleted = false;



    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform spawnPoint;

    public delegate void BallEvent();
    public event BallEvent OnBallDestroyed;

    public bool IsLevelCompleted()
    {
        return levelCompleted;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetStateAndRespawnBall()
    {
        GameObject[] existingBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in existingBalls)
        {
            Destroy(ball);
        }

        ballCount = 0;
        gameOver = false;
        levelStarted = false;
        levelCompleted = false;


        Invoke(nameof(SpawnBall), 0.2f);
        Invoke(nameof(EnableLevelCheck), 0.5f);
    }

    public void SpawnBall()
    {
        if (ballPrefab != null && spawnPoint != null)
        {
            Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        }
        else
        {
            Debug.LogError("Ball prefab or spawn point not assigned!");
        }
    }

    void EnableLevelCheck()
    {
        levelStarted = true;
    }

    public void RegisterBall()
    {
        ballCount++;
        Debug.Log("Ball Registered. Total: " + ballCount);
    }

    public void UnregisterBall()
    {
        ballCount--;
        Debug.Log("Ball Destroyed. Remaining: " + ballCount);
        OnBallDestroyed?.Invoke();

        if (levelStarted && ballCount <= 0 && !gameOver && !levelCompleted)
        {
            Debug.Log("All balls finished – Level Complete");
            levelCompleted = true;
            FindObjectOfType<UIController>().ShowLevelCompletePanel();
        }
    }



    public void SetGameOver()
    {
        if (gameOver || levelCompleted || !levelStarted)
        {
            Debug.Log("❌ Game Over skipped - Already completed or not started.");
            return;
        }

        gameOver = true;
        Debug.Log("❗ Game Over Triggered");
        FindObjectOfType<UIController>().ShowGameOver();
    }


}
