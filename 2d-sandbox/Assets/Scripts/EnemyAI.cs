using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 3f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.

	[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Transform m_CeilingCheck;   // A position marking where to check for ceilings
	const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
	private Animator m_Anim;            // Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	public Transform _player;
	public Vector3 _target;


	float dirX;

	Rigidbody2D rb;
	bool facingRight = false;
	Vector3 localScale;


	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D> ();
		m_Anim = GetComponent<Animator>();

	}

	void Update () {
		//player's x is target for bot
		_target.x = _player.transform.position.x;

		//MOVING WITH ADDFORCE

		if (_target.x - transform.position.x > 0) {
			rb.AddForce (Vector2.right * m_MaxSpeed);
			dirX = 1f;
		} else if (_target.x - transform.position.x < 0) {
			rb.AddForce (Vector2.left * m_MaxSpeed);
			dirX = -1f;
		}
		m_GroundCheck = transform.Find("GroundCheck");
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}


		
	}

	void OnTriggerEnter2D (Collider2D col)  					//Перепрыгивать препятствия
	{
		switch (col.tag) {
		case "Block":
			{
				if (m_Grounded)
					rb.velocity = Vector2.up * m_JumpForce;
			}

			break;
		}
	}

	private void Awake()
	{
		// Setting up references.

		m_CeilingCheck = transform.Find("CeilingCheck");
		m_Anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}


	// Поворот морды 
	void LateUpdate()
	{
		if (dirX > 0)
			m_FacingRight = true;
		else if (dirX < 0)
			m_FacingRight = false;

		if (((m_FacingRight) && (localScale.x < 0)) || ((!m_FacingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}

