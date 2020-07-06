using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to Control the Player's Behaviour
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject playerHitPrefab;
    public AudioClip throwSound;
    public AudioClip hitSound;
    public Text ammoText;

    public int Health { get; private set; }

    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvicible = 2.0f;
    public float projectileForce = 300.0f;
    public int ammo = 10;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    AudioSource audioSource;

    float horizontal;
    float vertical;
    float invicibleTimer;
    bool isInvicible;

    /// <summary>
    /// Sets the Components and Health to maxHealth (5), and Ammo to ammo (10)
    /// </summary>
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Health = maxHealth;
        AmmoUpdate(ammo);
    }

    /// <summary>
    ///  Sets the Behaviour of the Player Every Second Frame
    /// </summary>
    void Update()
    {

        // Get Inputs to move the Player
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        // If move is greater than 1, set the direction
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        // Set the Animations
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // To make the Player invicible for a limited amount of time, if the Player is Damage
        if (isInvicible)
        {
            invicibleTimer -= Time.deltaTime;
            if (invicibleTimer < 0)
                isInvicible = false;
        }

        // If "K", Launch the Projectile
        if (Input.GetButtonDown("Fire1"))
            Launch();

        // If "K" is input and is within the NPC, Display the Dialog 
        if (Input.GetKeyDown(KeyCode.K))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                var character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }

    /// <summary>
    /// Moves the Player to the position every Fixed Frame
    /// </summary>
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * horizontal * Time.fixedDeltaTime;
        position.y += speed * vertical * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    /// <summary>
    /// Changes the Health and Sets the Player to Invicible
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvicible)
                return;

            isInvicible = true;
            invicibleTimer = timeInvicible;

            PlaySound(hitSound);
            Instantiate(playerHitPrefab, rigidbody2d.position, Quaternion.identity);

        }

        // Clamps the Health to the Min and Max values
        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        // Sets the Health in the UI
        UIHealthBar.Instance.SetValue(Health / (float)maxHealth);
    }

    /// <summary>
    /// Plays an Audio Clip one shot
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Instantiates and ProjectilePrefab and set the Direction
    /// </summary>
    void Launch()
    {
        // Ammo is used to limit the instantiation
        if (ammo > 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, projectileForce);
            animator.SetTrigger("Launch");
            PlaySound(throwSound);
            ammo--;
        }
        AmmoUpdate(ammo);
    }

    /// <summary>
    /// Adds Ammo to the Player
    /// </summary>
    /// <param name="ammo"></param>
    void AmmoUpdate(int ammo)
    {
        ammoText.text = "x " + ammo;
    }

    /// <summary>
    /// If Player Collides with Ammo GameObject, Update the Ammo
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            ammo += 10;
            AmmoUpdate(ammo);
            Destroy(other.gameObject);
        }
    }

}
