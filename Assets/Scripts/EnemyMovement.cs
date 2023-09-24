using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2.0f;
<<<<<<< Updated upstream
    [SerializeField] private int damageGiven = 1;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);

        if(moveSpeed > 0) 
        {
            rend.flipX = false;
        }

        if(moveSpeed < 0) 
        {
            rend.flipX = true;
        }
=======
    private SpriteRenderer rend;

    
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();  
    }

    
    void FixedUpdate()
    {
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);   

        if(moveSpeed > 0 ) 
        {
            rend.flipX = false;
        }
        else
        {
            rend.flipX = true;
        }

>>>>>>> Stashed changes
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
<<<<<<< Updated upstream
        if(other.gameObject.CompareTag("EnemyBlock"))
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
}
=======
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }
    }

}


>>>>>>> Stashed changes
