using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private WebCamTexture cam;
    [SerializeField]
    private RawImage backgroundTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.HasUserAuthorizedPermission(Permission.Camera);
        }

        //primero: Revisar camaras de nuestro dispositivo
        WebCamDevice[] realCamaras = WebCamTexture.devices;

        for(int i = 0; i < realCamaras.Length; i++)
        {
            Debug.Log(realCamaras[i].name);
            if (realCamaras[i].isFrontFacing == false)
            {
                cam = new WebCamTexture(realCamaras[i].name, Screen.width, Screen.height);
                break;
            }
        }

        cam.Play();
        backgroundTexture.texture = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
