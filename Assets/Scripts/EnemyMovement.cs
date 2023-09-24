using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2.0f;
    private SpriteRenderer rend;
    [SerializeField] private int damageGiven = 1;

    private int ghoulHealth = 3;
    private bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);

        if(moveSpeed > 0 ) 
        {
            rend.flipX = false;
        }
        else
        {
            rend .flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);
        }

    }


    public void TakeDamage()
    {
        canMove = false;
        ghoulHealth--;
        Invoke("CanMoveAgain", 1f);

            if (ghoulHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void CanMoveAgain()
        {
            canMove = true;
        }


}
