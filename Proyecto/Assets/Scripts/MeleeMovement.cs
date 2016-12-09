using UnityEngine;
using System.Collections;

public class MeleeMovement : MonoBehaviour 
{
	public Animator anim;
	public Rigidbody rbody;
	public Transform trnsfrm;
	public float speedH = 50, speedV = 50, runSpeed = 100, JumpTime = 1;
	float InputH, InputV, moveZ, turn;
	[SerializeField] float JumpPower = 12f;
	bool jump, Run, punch, kick;
	float move;
	public bool caos = false;
	public int dummies = 1;
	public int dummies_aux = 0;
	public Collider[] attackHitboxes;
	public int caos_time = 1000;
	private int caos_time_aux = 0;
	public bool complete = false;



	//-------------------------------------------------------------------- START

	void Start () 
	{
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		trnsfrm = GetComponent<Transform> ();
		Run = false;
		punch = kick = true;
	}

	//-------------------------------------------------------------------- UPDATE

	void Update ()
	{
		if(Input.GetKey(KeyCode.LeftShift))					//------------ VERIFICACION DE CORRER
		{
			Run = true; 
		}
		else
		{
			Run = false;
		}

		InputH = Input.GetAxis ("Horizontal");				//------------- MOVIMIENTO
		InputV = Input.GetAxis ("Vertical");

		anim.SetFloat ("InputH", InputH);
		anim.SetFloat ("InputV", InputV);
		anim.SetBool ("Run", Run);

		if(Run)												//------------- APLICACION DE CORRER
		{
			move = InputV * runSpeed * Time.deltaTime;
		}
		else
		{
			move = InputV * speedV * Time.deltaTime;
		}


		if (Input.GetKeyDown (KeyCode.Space)) 				//------------- VERIFICACION DE SALTO
		{
			anim.SetBool ("Jump", true);
			jump = true;
		} 
		else 
		{ 
			anim.SetBool ("Jump", false);
			jump = false;
		}
			

		float moveX = Mathf.Sin (trnsfrm.eulerAngles.y * (Mathf.PI / 180)) * move;	//---- ASIGNACION DE MOVIMIENTO EJE X
		float moveZ2 = Mathf.Cos (trnsfrm.eulerAngles.y * (Mathf.PI / 180)) * move; //---- ASIGNACION DE MOVIMIENTO EJE Z

		turn = InputH * speedH * Time.deltaTime;          //--------------- ROTACION

		rbody.velocity = new Vector3 (moveX, 0, moveZ2); //-------------- APLICACION DE MOVIMIENTO
		trnsfrm.Rotate (0, turn, 0);					//--------------- APLICACION DE ROTACION

		if (jump) 
		{
			rbody.velocity = new Vector3 (0, JumpPower, 0); //-------- APLICACION DE SALTO
		}
			

		/*
		if(contadorSalto < JumpTime && jump) 
		{
			rbody.velocity = new Vector3(rbody.velocity.x, JumpPower, rbody.velocity.z);
			contadorSalto += Time.deltaTime;
			air = true;
		}

		else if (contadorSalto >= JumpTime || air) 
		{
			rbody.velocity = new Vector3 (rbody.velocity.x, -JumpPower, rbody.velocity.z);
			contadorSalto -= Time.deltaTime;
		}

		if (contadorSalto <= 0)
		{
			air = false;
			contadorSalto = 0;
		}
		*/
		
		
		if(caos_time < caos_time_aux)
		{
			caos = false;
			caos_time_aux = 0;
		}
		else {
			caos_time_aux = caos_time_aux + 1;
		}

		if(Input.GetKeyDown(KeyCode.A))			//------ VERIFICACION DE GOLPE
		{
			caos_time_aux = 0;
			caos = true;
			if (punch) 
			{
				punch = false;
				anim.Play ("punchRt", -1, 0f);		//---- ANIMACION DE GOLPE DERECHO
			}
			else 
			{
				punch = true;
				anim.Play ("punchLf", -1, 0f);		//---- ANIMACION DE GOLPE IZQUIERDO
			}
		}

		if (Input.GetKeyDown(KeyCode.S))		//------ VERIFICACION DE PATADA
		{
			caos_time_aux = 0;
			caos = true;
			//Ataque(attackHitboxes[1]);
			if (kick)
			{
				kick = false;
				anim.Play ("kickRt", -1, 0f);	//----- ANIMACION DE PATADA DERECHA
			}
			else
			{
				kick = true;
				anim.Play ("kickLf", -1, 0f);   //------ ANIMACION DE PATADA IZQUIERDA
			}
		}
	}

	private void Ataque (Collider col)
	{
		//print(col.bounds.center);
		//print(col.bounds.extents);
		//print(col.transform.rotation);
		//print(LayerMask.GetMask("Hitbox"));
		Collider[] cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
		foreach( Collider c in cols)
		{
			print(c);
		}

	}

}