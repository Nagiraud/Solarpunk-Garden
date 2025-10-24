using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject prefab_seed;
    public GameObject spawner;
    private MeshRenderer renderer;
    private float table;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = spawner.GetComponent<MeshRenderer>();
        Debug.Log(renderer.bounds.size.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
