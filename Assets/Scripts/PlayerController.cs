using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private int count;

	public float horizontalspeed;
	public float verticalspeed;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
	void FixedUpdate()
	{
        if (GameManager.instance.isGameOver)
            return;

        float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        if (moveVertical < 0)
            moveVertical *= .8f;
		Vector2 movement = new Vector2 (moveHorizontal*horizontalspeed, moveVertical*verticalspeed);
        rb.AddForce (movement);
        rb.AddTorque(rb.rotation*-0.1f);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.instance.isGameOver)
            return;
        int damage = (int)(0.5f * 0.1f * rb.mass * other.relativeVelocity.magnitude * other.relativeVelocity.magnitude);
        //Debug.Log("Relative velocity: " + other.relativeVelocity.magnitude);
        //Debug.Log("I have mass of: " + rb.mass);
        //Debug.Log("Energy of Impact: " + damage);
        if (damage >= 2)
        {
            GameManager.instance.DamagePlayer(damage);
            GameManager.instance.gui.ActivateDamagePopup(gameObject.transform, damage);
        }
    }
}