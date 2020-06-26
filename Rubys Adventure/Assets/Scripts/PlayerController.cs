using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Health { get; private set; }
    
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvicible = 2.0f;

    Rigidbody2D rigidbody2d;

    float horizontal;
    float vertical;
    float invicibleTimer;
    bool isInvicible;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvicible)
        {
            invicibleTimer -= Time.deltaTime;
            if (invicibleTimer < 0)
                isInvicible = false;
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * horizontal * Time.fixedDeltaTime;
        position.y += speed * vertical * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);
    }

    //Add Health
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvicible)
                return;

            isInvicible = true;
            invicibleTimer = timeInvicible;
        }

        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        Debug.Log(Health + "/" + maxHealth);
    }

}
