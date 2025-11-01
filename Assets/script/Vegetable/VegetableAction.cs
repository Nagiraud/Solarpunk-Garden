
using System.Collections;
using UnityEngine;

// Gére la pousse du légume
public class VegetableAction : MonoBehaviour
{

    private GrowingArea parentGrowingArea; // Référence à la zone de plantation
    private float growDuration;
    public float growTimer = 0f;

    void Start()
    {
        growDuration = Random.Range(30f, 300f); // entre 1 et 5 minutes
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow_Coroutine());
    }

    // Méthode pour définir la zone parente
    public void SetGrowingArea(GrowingArea area)
    {
        parentGrowingArea = area;
    }


    IEnumerator Grow_Coroutine()
    {
        // Croissance

        while (growTimer < growDuration) 
        {
            growTimer += Time.deltaTime;
            
            if(transform.localScale.x < 3.0f)// taille max
            {
                transform.localScale += Vector3.one * 0.045f * Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
 

        // Croissance maximum atteind
        StopCoroutine(Grow_Coroutine());
        parentGrowingArea.ExplodeVegetable(); //retire la référence
        Destroy(gameObject);
        
    }

    public float GetTimeGrowing()
    {
        return growTimer;
    }
}
