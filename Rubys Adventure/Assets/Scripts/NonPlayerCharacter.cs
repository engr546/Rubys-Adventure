using UnityEngine;

/// <summary>
/// Script to Control the NPC's Behaviour
/// </summary>
public class NonPlayerCharacter : MonoBehaviour
{

    public float displayTime = 4.0f;
    public GameObject dialogBox;

    float timerDisplay;

    /// <summary>
    /// Disables the Dialog Box and sets timerDisplay to less than 0
    /// </summary>
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    /// <summary>
    /// if timerDisplay > 0, Deduct the timerDisplay and Set the Dialog Box to false
    /// </summary>
    void Update()
    {
        if (timerDisplay > 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
                dialogBox.SetActive(false);
        }
    }

    /// <summary>
    /// Display the Dialog Box and Set timerDisplay to displayTime (4.0f)
    /// </summary>
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

}
