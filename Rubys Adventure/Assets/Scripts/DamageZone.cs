using UnityEngine;

/// <summary>
/// Script to Damage the Health of the Player
/// </summary>
public class DamageZone : MonoBehaviour
{
    /// <summary>
    /// If Player Collides with this GameObject, Deduct Health to Player
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
            controller.ChangeHealth(-1);
    }
}
