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
        playerInput = gameObject.AddComponent<PlayerInput>();

        var actionMap = new InputActionMap("MouseDrag");

        dragAction = actionMap.AddAction("Drag", InputActionType.Button);
        dragAction.AddBinding("<Mouse>/leftButton");

        deltaAction = actionMap.AddAction("Delta", InputActionType.Value);
        deltaAction.AddBinding("<Mouse>/delta");

        actionMap.Enable();

        dragAction.started += ctx => isDragging = true;
        dragAction.canceled += ctx => isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 delta = deltaAction.ReadValue<Vector2>();

            transform.Rotate(0, -delta.x * rotationSpeed, 0, Space.World);
        }
    }

    private void OnEnable()
    {
        if (dragAction != null)
        {
            dragAction.Enable();
        }

        if (deltaAction != null)
        {
            deltaAction.Enable();
        }
    }

    private void OnDisable()
    {
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