using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DroneMovement : MonoBehaviour
{
    public float speed = 1;
    public InputActionReference droneMove;
    public InputActionReference droneLook;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = droneMove.action.ReadValue<Vector2>();
        Vector2 Rotation = droneLook.action.ReadValue<Vector2>();

        transform.Rotate(Vector3.up, Rotation.x * Time.deltaTime*5);
        GetComponent<CharacterController>().Move(transform.forward * Direction.y * speed);
    }
}
