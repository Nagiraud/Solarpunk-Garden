using System;
using UnityEngine;


// Gestion de la zone de plantation
// Gére l'état, la création et la destruction de son légume
public class GrowingArea : MonoBehaviour
{
    public GameObject prefabSeed; //prefab
    private GameObject seed;       // graine actuel
    public GameObject[] prefabVegetables = new GameObject[3];     // légume aléatoire
    private GameObject vegetable;       // Légume actuel
    private Collider CollidingArea;



    private const string TagPlantable = "Plantable";
    private const string TagOccuped = "Occuped";
    private const string TagGrowing = "Growing";
    private const string TagCarrot = "carrot";
    private const string TagCauliflower = "cauliflower";
    private const string TagCorn = "corn";
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

        float randomValue = UnityEngine.Random.Range(0f, 100f);

        // légume aléatoire
        if (randomValue < 50) {  // 50% de chances d'obtenir le mais
        //Instantiation en dessous du centre de la zone
            vegetable = Instantiate(prefabVegetables[0], CollidingArea.bounds.center + new Vector3(0, -1, 0), Quaternion.identity);
            vegetable.tag = TagCorn;
        }
        else if(randomValue< 85){ // 35% de chances d'obtenir la carrote
            vegetable = Instantiate(prefabVegetables[1], CollidingArea.bounds.center + new Vector3(0, -1, 0), Quaternion.identity);
            vegetable.tag = TagCauliflower;
        }
        else // 15% restant d'obtenir la carrote
        {
            vegetable = Instantiate(prefabVegetables[2], CollidingArea.bounds.center + new Vector3(0, -1, 0), Quaternion.identity);
            vegetable.tag = TagCarrot;
        }
        vegetable.AddComponent<VegetableAction>();
        this.tag = TagGrowing;

        // référence la zone (pour permettre de connaitre l'état actuel du légume dans cette zone)
        VegetableAction vegAction = vegetable.GetComponent<VegetableAction>();
        vegAction.SetGrowingArea(this);
    }

    // Récuperation du légume
    public void PickVegetable()
    {
        Debug.Log("Pick");
        float TimeVegetable=vegetable.GetComponent<VegetableAction>().GetTimeGrowing()/10;
        switch (vegetable.tag)
        {
            case TagCorn:
                Debug.Log(TimeVegetable);
                ScoreManager.Instance.AddScore((int)TimeVegetable); // Score classique pour les mais
                break;
             case TagCauliflower:
                Debug.Log(TimeVegetable);
                ScoreManager.Instance.AddScore((int)Mathf.Round(TimeVegetable * 2)); // *2 pour les choux
                break;
            case TagCarrot:
                Debug.Log(TimeVegetable);
                ScoreManager.Instance.AddScore((int)Mathf.Round(TimeVegetable * 4)); // *4 pour les carrot
                break;
        }
        Destroy(vegetable);
        
        this.tag = TagPlantable;
    }

    // Explosion du légume
    public void ExplodeVegetable()
    {
        vegetable = null;
        ScoreManager.Instance.RemoveScore(15);
        this.tag = TagPlantable;
    }
}
