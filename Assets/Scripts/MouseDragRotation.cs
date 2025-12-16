using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;

    private PlayerInput playerInput;
    private InputAction dragAction;
    private InputAction deltaAction;
    private bool isDragging = false;

    private void Awake()
    {
        // Crear les accions programàticament
        playerInput = gameObject.AddComponent<PlayerInput>();

        // Crear un Input Action Map
        var actionMap = new InputActionMap("MouseDrag");

        // Acció per detectar quan es manté premut el botó del mouse
        dragAction = actionMap.AddAction("Drag", InputActionType.Button);
        dragAction.AddBinding("<Mouse>/leftButton");

        // Acció per obtenir el delta del moviment del mouse
        deltaAction = actionMap.AddAction("Delta", InputActionType.Value);
        deltaAction.AddBinding("<Mouse>/delta");

        // Activar el mapa d'accions
        actionMap.Enable();

        // Subscriure's als events
        dragAction.started += ctx => isDragging = true;
        dragAction.canceled += ctx => isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            // Llegir el delta del moviment del mouse
            Vector2 delta = deltaAction.ReadValue<Vector2>();

            // Rotar l'objecte en l'eix Y (horitzontal) basant-se en el moviment X del mouse
            transform.Rotate(0, -delta.x * rotationSpeed, 0, Space.World);
        }
    }

    private void OnDestroy()
    {
        // Netejar les subscripcions
        if (dragAction != null)
        {
            dragAction.Disable();
            dragAction.Dispose();
        }

        if (deltaAction != null)
        {
            deltaAction.Disable();
            deltaAction.Dispose();
        }
    }
}