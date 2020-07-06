using UnityEngine;

/// <summary>
/// Script to Add Health to the Player
/// </summary>
public class HealthCollectible : MonoBehaviour
{

    public AudioClip collectibleClip;

    /// <summary>
    /// If GameObject collides with Player, add Health
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            if (controller.Health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                controller.PlaySound(collectibleClip);
                Destroy(gameObject);
            }
        }
    }

}
