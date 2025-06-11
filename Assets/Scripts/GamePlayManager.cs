using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    [Header("References")]
    public Player playerScript;
    public Chain chainScript;

    [Header("UI Elements")]
    public Text levelText;
    public Text timerText;

    [Header("Level Durations (seconds)")]
    public float[] levelDurations;

    private float timer;
    private bool levelRunning;

    void OnEnable()
    {
        if (BallTracker.instance != null)
            BallTracker.instance.OnBallDestroyed += OnBallDestroyed;
    }

    void OnDisable()
    {
        if (BallTracker.instance != null)
            BallTracker.instance.OnBallDestroyed -= OnBallDestroyed;
    }

    void Start()
    {
        // Optional: Initialize level if needed from Start
    }

    void Update()
    {
        if (!levelRunning || BallTracker.instance == null)
            return;

        if (BallTracker.instance.IsLevelCompleted())
        {
            Debug.Log("✅ Timer stopped because level is already completed.");
            levelRunning = false;
            return;
        }

        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0f)
        {
            Debug.Log("❗ Timer hit 0, calling SetGameOver");
            levelRunning = false;
            BallTracker.instance.SetGameOver();
        }
    }




    public void InitializeLevel(int levelIndex, int totalBallCount)
    {
        levelRunning = true;

        if (levelIndex >= 0 && levelIndex < levelDurations.Length)
            timer = levelDurations[levelIndex];
        else
            timer = 60f;

        levelText.text = "Level " + (levelIndex + 1);
        timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    void OnBallDestroyed()
    {
        Debug.Log("Ball Destroyed.");
    }

    public void MoveLeft()
    {
        playerScript.SetMoveDirection(-1);
    }

    public void MoveRight()
    {
        playerScript.SetMoveDirection(1);
    }

    public void StopMovement()
    {
        playerScript.SetMoveDirection(0);
    }

    public void FireChain()
    {
        Chain.IsFired = true;
    }

    void ResetGameplayUI()
    {
        levelText.text = "";
        timerText.text = "";
    }

    public void StopGameplay()
    {
        levelRunning = false;
        timer = 0f;
        Debug.Log("Gameplay stopped and timer reset.");
    }


    public void SetMoveDirection(int dir)
    {
        playerScript.SetMoveDirection(dir);
    }


}
