using UnityEngine;

public class GiroscopioController : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SystemInfo.supportsGyroscope == true) // nos devuelve la rot del dispositivo
        {
            Quaternion inputGyro = Input.gyro.attitude;

            //invertimos el eje z y w del quaternion para que la rotacion del giroscopio encaje con la de la cam en coordenadas de Unity.
            //cam.rotation = new Quaternion(inputGyro.x, inputGyro.y, - inputGyro.z, -inputGyro.w);

            Quaternion correcionGiro = Quaternion.Euler (90, 0, 0);
            cam.rotation = correcionGiro * new Quaternion(inputGyro.x, inputGyro.y, -inputGyro.z, -inputGyro.w);
        }
    }
}
