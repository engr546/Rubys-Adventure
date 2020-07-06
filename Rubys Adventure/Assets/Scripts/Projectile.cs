using UnityEngine;

/// <summary>
/// Setups and Controls the Behaviour of Projetile Prefab
/// </summary>
public class Projectile : MonoBehaviour
{

    public float timer = 3.0f;
    
    new Rigidbody2D rigidbody2D;

    /// <summary>
    /// Awake runs when the Object is Created; Start runs at the First Frame
    /// Sets the Component
    /// </summary>
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Destroy the Object with a given time (3.0f)
    /// </summary>
    void Update()
    {
        Destroy(gameObject, timer);
    }

    /// <summary>
    /// Launches the Projectile at a given direction and force
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    /// <summary>
    /// If Projectile Collides with Enemy, Set the Enemy to Fix Method
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemyController = other.collider.GetComponent<EnemyController>();
        if (enemyController != null)
            enemyController.Fix();
        
        Destroy(gameObject);
    }

}
