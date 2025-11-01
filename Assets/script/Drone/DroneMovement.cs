using UnityEngine;
using UnityEngine.InputSystem;

public class DroneMovement : MonoBehaviour
{
    // Actions
    [Header("Input")]
    public InputActionReference droneMove;
    public InputActionReference plantVegetable;
    public InputActionReference SprinkleVegetable; // Arroser

    // Objets
    private GameObject ObjectTrigger;

    // Variables
    [Header("Variable")]
    public float speed = 1;
    public int NumberSeed=5;
    private const string TagPlantable= "Plantable";
    private const string TagOccuped = "Occuped";
    private const string TagGrowing = "Growing";
    private const string TagShop = "Shop";

    // Particule
    [Header("Particule")]
    public ParticleSystem water_particle;
    public ParticleSystem seed_particle;

    // Son du drone
    [Header("Son")]
    public AudioClip SoundPlant;        
    public AudioClip SoundSprinkle;

    void Start()
    {
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

        // empeche de sortir des limites
        Vector3 currentPos = transform.position;

        // Définir les limites
        float minX = -17f, maxX = 17f;
        float minZ = -16f, maxZ = 16.5f;

        // Vérifier chaque axe
        if (currentPos.x < minX || currentPos.x > maxX || // Haut / bas
            currentPos.z < minZ || currentPos.z > maxZ)  // Gauche / Droite
        {

            // Recaler le joueur
            currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
            currentPos.z = Mathf.Clamp(currentPos.z, minZ, maxZ);
            transform.position = currentPos;

        }
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
                        seed_particle.Play();
                        AudioSource.PlayClipAtPoint(SoundPlant,transform.position,1.0f);
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
        }
        // Peut arroser n'importe ou
        water_particle.Play(); 
        AudioSource.PlayClipAtPoint(SoundSprinkle, transform.position,1.0f);
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
