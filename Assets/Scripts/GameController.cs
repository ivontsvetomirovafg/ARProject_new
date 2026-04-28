using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input Manager --> Lo he llamado directamente en el propio GiroscopioController.
        if (Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);

            //if (touch.phase ==TouchPhase.Began) //El primer frame que el dedo toca la pantalla.
            {

            }

            /*
            if (touch.phase ==TouchPhase.Moved) //Detecta si el dedo está en una pos distinta del frame anterior.
            {

            }

            if (touch.phase == TouchPhase.Stationary) //Mira si el dedo está en la misma pos que el frame anterior.
            {

            }

            if (touch.phase == TouchPhase.Ended) //El frame después de que el dedo haya dejado de tocar la pantalla.
            {

            }

            if (touch.phase == TouchPhase.Canceled) // El frame después de que el dedo haya dejado de tocar la pantalla.
            {

            }

            //touch.position; // Pos en pixeles de la pantalla del dedo.
            */
        }

        //Input System
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 touchPos = playerInput.actions["TouchPosition"].ReadValue<Vector2>();
        }
        
        /*if (context.phase == InputActionPhase.Performed)
        {

        }

        if (context.phase == InputActionPhase.Canceled)
        {

        }*/
    }
}
