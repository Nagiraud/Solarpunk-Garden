using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Gére le démarage,la pause, et la sortie de jeu
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance; // Singleton

    [Header("World")]
    public GameObject prefab_plantable;

    [Header("Pause")]
    public GameObject pauseMenuUI;
    public InputActionReference InputPause;
    public Button resumeButton;
    public bool IsPaused { get; set; }

    [Header("Grid Settings")]
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float spacing = 2f;
    public Vector3 startPosition = new Vector3(-4f, 0f, -4f);

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

    private void OnEnable()
    {
        InputPause.action.performed += MenuPause;
    }

    private void OnDisable()
    {
        InputPause.action.performed -= MenuPause;
    }

    void Start()
    {
        //menu pause désactivé
        pauseMenuUI.SetActive(false);

        // Création du terrain
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 position = new Vector3(
                    startPosition.x + x * spacing,
                    startPosition.y+1,
                    startPosition.z + z * spacing
                );

                GameObject pp = Instantiate(prefab_plantable, position, Quaternion.identity);
                pp.layer = 6;
            }
        }
    }

    public void MenuPause(InputAction.CallbackContext _ctx)
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            AudioListener.pause = true; // Met aussi l'audio en pause
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            AudioListener.pause = false;
        }
    }

}
