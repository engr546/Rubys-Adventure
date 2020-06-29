using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public ParticleSystem smokeEffect;
    public float changeTime = 3.0f;
    public float speed;
    public bool vertical;

    new Rigidbody2D rigidbody2D;
    Animator animator;

    float timer = 1;
    int direction = 1;
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
            return;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }

    void FixedUpdate()
    {
        if (!broken)
            return;

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y += Time.fixedDeltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x += Time.fixedDeltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }

        rigidbody2D.MovePosition(position);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
            player.ChangeHealth(-1);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

}
