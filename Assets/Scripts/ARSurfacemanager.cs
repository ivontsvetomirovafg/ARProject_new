using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARSurfacemanager : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager planeManager;
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private GameObject canvasUI;
    private bool planeVisibility = true;
    private PlayerInput playerInput;

    private int selectedIndex; 

    [SerializeField]
    private AudioClip[] cats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = planeVisibility;
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 touchPos = playerInput.actions["TouchPosition"].ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Choco contra " + hit.transform.name);
                AudioManager.instance.PlaySFX(cats[selectedIndex], transform.position);
                Instantiate(prefabs[selectedIndex], hit.point, Quaternion.identity);
            }
        }
    }

    public void ToggleVisibilityButton()
    {
        canvasUI.SetActive(false);  
        planeVisibility = !planeVisibility;
    }

    public void SelectObject(int index) 
    {
        selectedIndex = index;
    }
}
