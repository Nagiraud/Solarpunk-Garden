using System.Collections;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private GrowingArea parentGrowingArea; // Référence à la zone de plantation
    private float growthDuration;
    private float growthTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        growthDuration = Random.Range(20f, 120f);
        StartCoroutine(Grow_Seed_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Méthode pour définir la zone parente
    public void SetGrowingArea(GrowingArea area)
    {
        parentGrowingArea = area;
    }

    IEnumerator Grow_Seed_Coroutine()
    {
        // Croissance

        while (growthTimer < growthDuration)
        {
            growthTimer += Time.deltaTime;
            transform.localScale += Vector3.one * 0.1f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Croissance maximum atteind
        StopCoroutine(Grow_Seed_Coroutine());

        Destroy(gameObject);

    }
}
