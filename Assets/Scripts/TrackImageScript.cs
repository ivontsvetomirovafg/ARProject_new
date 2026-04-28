using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;
using UnityEngine.UI;

public class TrackImageScript : MonoBehaviour
{
    private bool pelea = false;
    private bool pelea2 = false;
    private bool pelea3 = false;

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;
    [SerializeField]
    private ARObjects[] objetosAR;

    [Header ("Primera pelea")]
    private GameObject prefabCopy;
    private GameObject prefabCopy2;

    [Header ("Segunda pelea")]
    private GameObject prefabCopy3;
    private GameObject prefabCopy4;

    [Header ("Tercera pelea")]
    private GameObject prefabCopy5;
    private GameObject prefabCopy6;

    //Animaciones
    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private Animator animator4;
    private Animator animator5;
    private Animator animator6;

    [SerializeField]
    private AudioClip fight1;
    [SerializeField]
    private AudioClip fight2;
    [SerializeField]
    private AudioClip fight3;

    public Text texto;

    private void OnEnable()
    {
        //trackedImagemanager.trackedImagesChanged += OnTrackedChanged; //Sirve para enlazar acciones. Herramienta para ponerlo todo en com�n (llamas a un solo evento).
        trackedImageManager.trackablesChanged.AddListener(OnTrackedChanged);
    }

    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedChanged);
    }
    private void Update()
    {
        if (prefabCopy != null && prefabCopy2 != null && pelea == false)
        {
            pelea = true;
            StartCoroutine(Pelea());

            prefabCopy.transform.LookAt(prefabCopy2.transform);
            prefabCopy2.transform.LookAt(prefabCopy.transform);
            
            animator1.SetBool("Fight", true);
            animator2.SetBool("Fight", true);
            Debug.Log(animator1.gameObject.name + " " + prefabCopy.name);
            Debug.Log(animator2.gameObject.name + " " + prefabCopy2.name);
            AudioManager.instance.PlaySFX(fight1, transform.position);
        }

        if (prefabCopy3 != null && prefabCopy4 != null && pelea2 == false)
        {
            pelea2 = true;
            StartCoroutine(Pelea2());

            prefabCopy3.transform.LookAt(prefabCopy4.transform);
            prefabCopy4.transform.LookAt(prefabCopy3.transform);

            animator3.SetBool("Fight", true);
            animator4.SetBool("Fight", true);

            Debug.Log(animator3.gameObject.name + " " + prefabCopy3.name);
            Debug.Log(animator4.gameObject.name + " " + prefabCopy4.name);        
            AudioManager.instance.PlaySFX(fight2, transform.position);
        }

        if (prefabCopy5 != null && prefabCopy6 != null && pelea3 == false)
        {
            pelea3 = true;
            StartCoroutine(Pelea3());

            prefabCopy5.transform.LookAt(prefabCopy6.transform);
            prefabCopy6.transform.LookAt(prefabCopy5.transform);

            animator5.SetBool("Fight", true);
            animator6.SetBool("Fight", true);

            Debug.Log(animator5.gameObject.name + " " + prefabCopy5.name);
            Debug.Log(animator6.gameObject.name + " " + prefabCopy6.name);
            AudioManager.instance.PlaySFX(fight3, transform.position);
        }
    }
    IEnumerator Pelea()
    {     
        yield return new WaitForSeconds(15f);
        
        animator1.SetBool("Fight", false);
        animator2.SetBool("Fight", false);

        animator2.SetTrigger("Win");
        animator1.SetTrigger("Die");
    }
    IEnumerator Pelea2()
    {
        yield return new WaitForSeconds(15f);
        animator3.SetBool("Fight", false);
        animator4.SetBool("Fight", false);

        animator4.SetTrigger("Win");
        animator3.SetTrigger("Die");
    }
    IEnumerator Pelea3()
    {
        yield return new WaitForSeconds(15f);
        animator5.SetBool("Fight", false);
        animator6.SetBool("Fight", false);

        animator5.SetTrigger("Win");
        animator6.SetTrigger("Die");
    }

    void OnTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventargs)
    {
        foreach (var newImage in eventargs.added) //para repasar todas las img que se han a�adido.
        {
            for(int i = 0; i < objetosAR.Length; i++)
            {
                if (objetosAR[i].referenceImageName == newImage.referenceImage.name)
                {
                    if (prefabCopy == null)
                    {
                        prefabCopy = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);

                        //prefabCopy = Instantiate(objetosAR[i].prefab, newImage.transform);
                        //prefabCopy.transform.localPosition = Vector3.zero;
                        //prefabCopy.transform.localRotation = Quaternion.identity;

                        animator1 = prefabCopy.GetComponent<Animator>();
                        texto.text = prefabCopy.transform.localScale.ToString();
                    }
                    else if (prefabCopy2 == null)
                    {
                        prefabCopy2 = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                        animator2 = prefabCopy2.GetComponent<Animator>();
                    }
                    else if (prefabCopy3 == null)
                    {
                        prefabCopy3 = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                        animator3 = prefabCopy3.GetComponent<Animator>();
                    }
                    else if (prefabCopy4 == null)
                    {
                        prefabCopy4 = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                        animator4 = prefabCopy4.GetComponent<Animator>();
                    }
                    else if (prefabCopy5 == null)
                    {
                        prefabCopy5 = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                        animator5 = prefabCopy5.GetComponent<Animator>();
                    }
                    else 
                    {
                        prefabCopy6 = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                        animator6 = prefabCopy6.GetComponent<Animator>();
                    }
                }
            }
            
        }

        foreach (var newImage in eventargs.removed) //por si la imagen no se trackea
        {
            //Eliminar el prefab
            /*if (newImage.referenceImage.name == "simpleFrame")
            {
                Destroy(prefabCopy);
            }*/
        }

        foreach (var newImage in eventargs.updated)
        {
            //Esto es cada frame que sigue detectando
            
            for (int i = 0; i < objetosAR.Length; i++)
            {
                if (objetosAR[i].referenceImageName == newImage.referenceImage.name && prefabCopy == null)
                {
                    prefabCopy = Instantiate(objetosAR[i].prefab, newImage.transform.position, newImage.transform.rotation);
                }
            }
        }
    }
}

[Serializable]
public class ARObjects
{
    public string referenceImageName;
    public GameObject prefab;
}
