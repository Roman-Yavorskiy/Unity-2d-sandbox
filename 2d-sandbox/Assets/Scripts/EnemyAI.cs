using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 3f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
	[SerializeField] private float xStart = 1f;
	[SerializeField] private float xFinish = 10f;
	//[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
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


	float dirX;
	//[SerializeField]
	//float moveSpeed = 3f;
	Rigidbody2D rb;
	bool facingRight = false;
	Vector3 localScale;


	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D> ();
		m_Anim = GetComponent<Animator>();
		//dirX = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < xStart)
			dirX = 1f;
		else if (transform.position.x > xFinish)
			dirX = -1f;
		
	}

	private void Awake()
	{
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_CeilingCheck = transform.Find("CeilingCheck");
		m_Anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}


	//Бесконечная хотьба
	void FixedUpdate()
	{
		//rb.velocity = new Vector2 (dirX * moveSpeed, rb.velocity.y);
		if (dirX > 0) {
			rb.AddForce (Vector2.right * m_MaxSpeed);
		} else if (dirX < 0) {
			rb.AddForce (Vector2.left * m_MaxSpeed);
		}
			//m_Anim.SetFloat("Speed", Mathf.Abs(dirX));
	//	Debug.Log(dirX);
	//	Debug.Log(rb.velocity);
		//Debug.Log();
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


	void OnTriggerEnter2D (Collider2D col)  					//Перепрыгивать препятствия
	{
		switch (col.tag) {
		case "Block":
			
			rb.velocity = Vector2.up * m_JumpForce;

			break;
		}
	}


	//Анимация земли и прыжка
	private void FixedUpdate1()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}
		m_Anim.SetBool("Ground", m_Grounded);

		// Set the vertical animation
		m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
	}


	//Хотьба персонажа по кнопкам
	public void Move(float move, bool jump)
	{
		/* If crouching, check to see if the character can stand up
		if (!crouch && m_Anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		} 

		// Set whether or not the character is crouching in the animator
		m_Anim.SetBool("Crouch", crouch); */

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			//move = (crouch ? move*m_CrouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			} 
		}  
		// If the player should jump...
		if (m_Grounded && jump && m_Anim.GetBool("Ground"))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Anim.SetBool("Ground", false);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}




	
}

