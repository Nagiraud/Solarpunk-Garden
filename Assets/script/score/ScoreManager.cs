using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Gestion du score
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
        if (score > 1000)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    public void RemoveScore(int nb)
    {
        score -= nb;
        UpdateScore();
        if (score < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
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
