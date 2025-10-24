using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton
    public GameObject prefab_plantable;

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
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
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
}
