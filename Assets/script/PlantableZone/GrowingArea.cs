using System;
using UnityEngine;


// Gestion de la zone de plantation
// Gére l'état, la création et la destruction de son légume
public class GrowingArea : MonoBehaviour
{
    public GameObject prefabSeed; //prefab
    private GameObject seed;       // Légume actuel
    public GameObject prefabVegetable; //prefab
    private GameObject vegetable;       // Légume actuel
    private Collider CollidingArea;

    private const string TagPlantable = "Plantable";
    private const string TagOccuped = "Occuped";
    private const string TagGrowing = "Growing";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollidingArea = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Plantation de la graine
    public void PlantSeed()
    {
        //Instantiation en dessous du centre de la zone
        seed = Instantiate(prefabSeed, CollidingArea.bounds.center + new Vector3(0, -1, 0), Quaternion.identity); 
        this.tag = TagOccuped;
    }

    // Aprés arrosage
    public void GrowVegetable()
    {
        Destroy(seed);
        //Instantiation en dessous du centre de la zone
        vegetable = Instantiate(prefabVegetable, CollidingArea.bounds.center + new Vector3(0, -1, 0), Quaternion.identity);
        this.tag = TagGrowing;

        // référence la zone (pour permettre de connaitre l'état actuel du légume dans cette zone)
        VegetableAction vegAction = vegetable.GetComponent<VegetableAction>();
        vegAction.SetGrowingArea(this);
    }

    // Récuperation du légume
    public void PickVegetable()
    {
        Destroy(vegetable);
        ScoreManager.Instance.AddScore((int)Mathf.Round(1 * transform.localScale.x));
        this.tag = TagPlantable;
    }

    public void ExplodeVegetable()
    {
        vegetable = null;
        ScoreManager.Instance.RemoveScore(1);
        this.tag = TagPlantable;
    }
}
