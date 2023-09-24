using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMovement : MonoBehaviour
{
    //[SerializeField]
    //private float movementSpeed = 2.0f;
    //[SerializeField]
    //private float knockbackForce = 200f;
    //[SerializeField]
    //private float upwardForce = 100f;
    //[SerializeField]
    //private int ghoulHealth = 3;

    //private bool canMove = true;
    //private Rigidbody2D rigidbody;
    //private SpriteRenderer renderer;
    // Start is called before the first frame update
//    void Start()
//    {
//        rigidbody = GetComponent<Rigidbody2D>();
//        renderer = GetComponent<SpriteRenderer>();
//    }

//    void FixedUpdate()
//    {
//        if (canMove == true)
//        {
//            rigidbody.velocity = new Vector2(movementSpeed * Time.deltaTime, rigidbody.velocity.y);

//            if (movementSpeed > 0)
//            {
//                renderer.flipX = false;
//            }

//            if (movementSpeed < 0)
//            {
//                renderer.flipX = true;
//            }
//        }
//        else
//        {
//            rigidbody.velocity = Vector2.zero;
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        if (other.gameObject.CompareTag("Enemy Block"))
//        {
//            movementSpeed = -movementSpeed;
//        }
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            movementSpeed = -movementSpeed;
//        }
//        if (other.gameObject.CompareTag("Player"))
//        {
//            other.gameObject.GetComponent<PlayerMovement>().TakeDamage();
//            if (other.transform.position.x > transform.position.x)
//            {
//                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
//            }
//            else
//            {
//                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
//            }
//        }
//    }



    //Här är linjen för att fienden ska kunna ta skada.
    //TakeDamage() kallas genom spelarens script när de träffar en fiende. Jag har gjort så att när man slår en fiende så blir den stunnad i 1 sekund tills "canMove" sätts på igen.


//    public void TakeDamage()
//    {
//        canMove = false;
//        ghoulHealth--;
//        Invoke("CanMoveAgain", 1f);

//        if (ghoulHealth <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void CanMoveAgain()
//    {
//        canMove = true;
//    }

}
