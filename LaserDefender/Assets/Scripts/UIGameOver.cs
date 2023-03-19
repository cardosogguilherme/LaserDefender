using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetInstance().GetScore().ToString();
    }
}
