using UnityEngine;

/// <summary>
/// Script to Control the Enemy Behaviour
/// </summary>
public class EnemyController : MonoBehaviour
{

    public ParticleSystem smokeEffect;
    public ParticleSystem enemyHitEffect;
    public AudioSource brokenSound;
    public AudioSource fixedSound;

    public float changeTime = 3.0f;
    public float speed;
    public bool vertical;

    new Rigidbody2D rigidbody2D;
    Animator animator;

    float timer = 1;
    int direction = 1;
    bool broken = true;

    /// <summary>
    /// Sets the Components, and timer to changeTime (3.0f)
    /// </summary>
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    /// <summary>
    /// If not broken, change the direction when timer < 0 and set timer to changeTime (3.0f)
    /// </summary>
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

    /// <summary>
    /// if not broken, Set the position and direction depending to the vertical (bool) variable
    /// </summary>
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

    /// <summary>
    /// If enemy collides with player, Deduct health
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
            player.ChangeHealth(-1);
    }

    /// <summary>
    /// Method to Fix the Enemy
    /// </summary>
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        enemyHitEffect.Play();
        smokeEffect.Stop();
        fixedSound.Play();
        brokenSound.Stop();
    }

}
