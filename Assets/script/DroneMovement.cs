using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DroneMovement : MonoBehaviour
{
    // Actions
    public InputActionReference droneMove;
    public InputActionReference plantVegetable;
    public InputActionReference SprinkleVegetable; // Arroser

    // Objets
    private GameObject ObjectTrigger;
    private ParticleSystem ParticleSeed;

    // Variables
    public float speed = 1;
    public int NumberSeed=5;
    private const string TagPlantable= "Plantable";
    private const string TagOccuped = "Occuped";
    private const string TagGrowing = "Growing";
    private const string TagShop = "Shop";



    void Start()
    {
        ParticleSeed =GetComponent<ParticleSystem>();
        ScoreManager.Instance.UpdateNumberSeed(NumberSeed);
    }

    private void OnEnable()
    {
        plantVegetable.action.performed += Plant;
        SprinkleVegetable.action.performed += Sprinkle;
    }

    private void OnDisable()
    {
        plantVegetable.action.performed -= Plant;
        SprinkleVegetable.action.performed -= Sprinkle;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = droneMove.action.ReadValue<Vector2>();

        // Gauche / Droite
        transform.Rotate(Vector3.up, Direction.x * Time.deltaTime *speed*100);

        // Avant / Arrière
        transform.Translate(Vector3.forward * Direction.y * Time.deltaTime* speed);
    }

    // planter un légume
    void Plant(InputAction.CallbackContext _ctx)
    {
        if (ObjectTrigger != null) { 
            switch (ObjectTrigger.tag)
            {
                case TagPlantable: // Libre
                    if (NumberSeed > 0)
                    {
                        ObjectTrigger.GetComponent<GrowingArea>().PlantSeed();
                        RemoveSeed(1);
                        ParticleSeed.Play();
                    }
                    break;
                case TagGrowing: // Occupé
                    ObjectTrigger.GetComponent<GrowingArea>().PickVegetable();
                    Debug.Log("ramasser");
                    break;
                case TagShop: // Shop
                    Debug.Log("Shop");
                    AddSeed(5);
                    break;
                default:
                    break;

            }
        }
    }

    public void AddSeed(int nb)
    {
        NumberSeed += nb;
        ScoreManager.Instance.UpdateNumberSeed(NumberSeed);
    }
    public void RemoveSeed(int nb)
    {
        NumberSeed -= nb;
        ScoreManager.Instance.UpdateNumberSeed(NumberSeed);
    }
    

    // Arroser un légume
    void Sprinkle(InputAction.CallbackContext _ctx)
    {
        if (ObjectTrigger != null && ObjectTrigger.tag==TagOccuped)
        {
            ObjectTrigger.GetComponent<GrowingArea>().GrowVegetable();
            ParticleSeed.Play(); // Particule d'eau
        }
    }

    // Action a réaliser selon la box rencontré
    private void OnTriggerEnter(Collider other)
    {
        ObjectTrigger= other.gameObject;  
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectTrigger = null;
    }
}
