using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton
    private int score;
    public TMP_Text text_score;
    public TMP_Text text_seed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateScore();
    }

    public void AddScore(int nb)
    {
        score += nb;
        UpdateScore();
    }
    public void RemoveScore(int nb)
    {
        score -= nb;
        UpdateScore();
    }

    public void UpdateScore()
    {
        text_score.text = "Score: " + score.ToString();
    }


    public void UpdateNumberSeed(int NumberSeed)
    {
        text_seed.text = "Nombre de graines: " + NumberSeed.ToString();
    }
}
