using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public GameObject prefab_seed;

    private float table;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        table = GetComponent<MeshRenderer>().bounds.size.x;
        Debug.Log(table);
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate(prefab_seed, transform.position, Quaternion.identity);
    }
}
