using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    private static ScoreKeeper instance;

    private void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public ScoreKeeper GetInstance()
    {
        return instance;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int points)
    {
        score += points;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }
}

