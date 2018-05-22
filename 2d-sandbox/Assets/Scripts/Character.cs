using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

	public float speed;
	public KeyCode jumpKey = KeyCode.Space;
	public Sprite[] walkSprites;
	public Sprite idleSprite;
	public float jumpSpeed;
	RaycastHit2D hit2d;

	bool isWalking;
	bool isGrounded;

	RaycastHit2D hit;

	void Update () {
		Rigidbody2D r = GetComponent<Rigidbody2D> ();
		r.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, r.velocity.y);
		if (r.velocity.magnitude > 0.1f) {
			if (!isWalking) {
				StartCoroutine (Walk ());
			}
		}

		hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), Vector2.down);
		//Debug.Log (Vector2.Distance (new Vector2 (transform.position.x, transform.position.y), hit.collider.transform.position));
		if (hit.distance < 1.9f) {
			isGrounded = true;
			//Debug.Log ("On ground.");
		} else {
			isGrounded = false;
			//Debug.Log ("Off ground.");
		}

		if (isGrounded) {
			if (Input.GetKeyDown (jumpKey)) {
				r.velocity = new Vector2 (r.velocity.x, jumpSpeed);
			}
		}

		if (!isWalking) {
			GetComponent<SpriteRenderer> ().sprite = idleSprite;
		}
		if (Input.GetMouseButton (0)) {
			Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mouse2d = new Vector2 (mouse.x, mouse.y);
			Vector2 robot = new Vector2 (transform.position.x, transform.position.y); //robot position


			RaycastHit2D hit2d = Physics2D.Raycast (robot, mouse2d - robot, 1f);

			//Debug.DrawRay (robot, mouse2d);
			//Debug.Log (robot);
			//Debug.Log (mouse2d);
			Debug.DrawRay (robot, mouse2d - robot);
			Debug.Log (hit2d.collider);
			Debug.Log (hit2d.collider.gameObject);
			Debug.Log (hit2d.collider.gameObject.GetComponent<Tiledatas> ());

			if (hit2d.collider.gameObject.GetComponent<Tiledatas> () != null) {
				GetComponent<Inventory> ().Add (hit2d.collider.gameObject.GetComponent<Tiledatas>().tileType, 1);
				Destroy (hit2d.collider.gameObject);

			} 
		}

	}

		IEnumerator Walk() {
			isWalking = true;
			GetComponent<SpriteRenderer> ().sprite = walkSprites [0];
			yield return new WaitForSeconds (0.25f);
			GetComponent<SpriteRenderer> ().sprite = walkSprites [1];
			yield return new WaitForSeconds (0.25f);
			isWalking = false;
		}

	}
