using UnityEngine;
using System.Collections;

public class MusicScriptHappy : MonoBehaviour {

	public AudioSource audio;
    public AudioClip sugar;
	private bool caos_active;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		sugar = GetComponent<AudioClip> ();
		caos_active = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject lechero = GameObject.Find("El Lechero");
        MeleeMovement go = (MeleeMovement) lechero.GetComponent(typeof(MeleeMovement));
        if(!go.caos && !caos_active){
        	caos_active = true;
            audio.Play();
        }
        else if(go.caos){
        	caos_active = false;
			audio.Stop();
        }
	}
}
