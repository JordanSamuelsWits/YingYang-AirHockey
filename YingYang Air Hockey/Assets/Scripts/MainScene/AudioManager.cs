using UnityEngine;

// Serializable class for managing audio clips and playing them via AudioSource
[System.Serializable]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component

    [Header("Audio Clips")]
    public AudioClip puckCollisionClip; // Sound for puck collision
    public AudioClip goalClip; // Sound for scoring a goal
    public AudioClip lostGameClip; // Sound for losing the game
    public AudioClip wonGameClip; // Sound for winning the game
    public AudioClip portalActivationClip; // Sound for portal activation

    private void Awake()
    {
        // Ensure AudioSource component is attached to the GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Play the puck collision audio clip
    public void PlayPuckCollision()
    {
        if (puckCollisionClip != null)
            audioSource.PlayOneShot(puckCollisionClip);
        else
            Debug.LogWarning("Puck collision audio clip is not assigned in AudioManager.");
    }

    // Play the goal audio clip
    public void PlayGoal()
    {
        if (goalClip != null)
            audioSource.PlayOneShot(goalClip);
        else
            Debug.LogWarning("Goal audio clip is not assigned in AudioManager.");
    }

    // Play the lost game audio clip
    public void PlayLostGame()
    {
        if (lostGameClip != null)
            audioSource.PlayOneShot(lostGameClip);
        else
            Debug.LogWarning("Lost game audio clip is not assigned in AudioManager.");
    }

    // Play the won game audio clip
    public void PlayWonGame()
    {
        if (wonGameClip != null)
            audioSource.PlayOneShot(wonGameClip);
        else
            Debug.LogWarning("Won game audio clip is not assigned in AudioManager.");
    }

    // Play the portal activation audio clip
    public void PlayPortalActivation()
    {
        if (portalActivationClip != null)
            audioSource.PlayOneShot(portalActivationClip);
        else
            Debug.LogWarning("Portal activation audio clip is not assigned in AudioManager.");
    }
}
