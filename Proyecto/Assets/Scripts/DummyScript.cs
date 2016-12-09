using UnityEngine;
using System.Collections;

public class DummyScript : MonoBehaviour {

    public Collider collider_dummy;
    public Animator anim;
    public AudioSource audio;
    public AudioClip hitsound;
    public GameObject enemy;

    private GameObject lechero;
    private MeleeMovement go;


    public float velocidadMax;     
    public float xMax;
    public float zMax;
    public float xMin;
    public float zMin;
    public float vida;         
    private float x;
    private float z;
    private float tiempo;
    private float angulo;
    private bool caos_active;
 
     // Use this for initialization
     void Start () {
        collider_dummy = GetComponent<Collider> ();
        audio = GetComponent<AudioSource> ();
        anim = GetComponent<Animator> ();
        enemy = GetComponent<GameObject> ();
        x = Random.Range(-velocidadMax, velocidadMax);
        z = Random.Range(-velocidadMax, velocidadMax);
        angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
        transform.localRotation = Quaternion.Euler( 0, angulo, 0);
        caos_active = false;
        lechero = GameObject.Find("El Lechero");
        go = (MeleeMovement) lechero.GetComponent(typeof(MeleeMovement));
     }
     
        
        
    void OnTriggerStay(Collider collider_dummy) 
    {
        if((collider_dummy.name == "Kick" && Input.GetKeyDown(KeyCode.S)) || (collider_dummy.name == "Punch" && Input.GetKeyDown(KeyCode.A))) {
            go.caos = true;
            if(vida > 1){
                vida = vida - 1;
                anim.Play ("Hit");
                audio.Play();
            }
            else if(vida == 1) {
                vida = vida - 1;
                velocidadMax=0;
                audio.Play();
                anim.Play ("dead");   
                StartCoroutine(ExecuteAfterTime(10));
                if(go.dummies_aux == go.dummies)
                {
                    print("Nivel Completo");
                }   
                else {
                    go.dummies_aux++;
                }

            }
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
     
        Dead();
    }

    void Dead()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y-20, transform.localPosition.z + z);
    }


     void Update () {

        if(go.caos && !caos_active){
            caos_active = true;
            anim.Play ("run", -1, 0f);   
            velocidadMax = velocidadMax*3;
        }
        else if(!go.caos && caos_active){
            caos_active = false;
            anim.Play ("walk");   
            velocidadMax = velocidadMax/3;
        }
 
        tiempo += Time.deltaTime;
        

        if (transform.localPosition.x > xMax) {
            x = Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f; 
        }
        if (transform.localPosition.x < xMin) {
            x = Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
            transform.localRotation = Quaternion.Euler(0, angulo, 0); 
            tiempo = 0.0f; 
        }
        if (transform.localPosition.z > zMax) {
            z = Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
            transform.localRotation = Quaternion.Euler(0, angulo, 0); 
            tiempo = 0.0f; 
        }
        if (transform.localPosition.z < zMin) {
            z = Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f; 
        }
 
 
        if (tiempo > 1.0f) {
            x = Random.Range(-velocidadMax, velocidadMax);
            z = Random.Range(-velocidadMax, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) ;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
 
        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z + z);

     }
}
