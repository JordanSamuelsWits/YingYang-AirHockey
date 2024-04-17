using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip puckCollisionClip;
    public AudioClip goalClip;
    public AudioClip lostGameClip;
    public AudioClip wonGameClip;
    public AudioClip portalActivationClip;

    private void Awake()
    {
        // Ensure AudioSource component is attached to the GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayPuckCollision()
    {
        if (puckCollisionClip != null)
            audioSource.PlayOneShot(puckCollisionClip);
        else
            Debug.LogWarning("Puck collision audio clip is not assigned in AudioManager.");
    }

    public void PlayGoal()
    {
        if (goalClip != null)
            audioSource.PlayOneShot(goalClip);
        else
            Debug.LogWarning("Goal audio clip is not assigned in AudioManager.");
    }

    public void PlayLostGame()
    {
        if (lostGameClip != null)
            audioSource.PlayOneShot(lostGameClip);
        else
            Debug.LogWarning("Lost game audio clip is not assigned in AudioManager.");
    }

    public void PlayWonGame()
    {
        if (wonGameClip != null)
            audioSource.PlayOneShot(wonGameClip);
        else
            Debug.LogWarning("Won game audio clip is not assigned in AudioManager.");
    }

    public void PlayPortalActivation()
    {
        if (portalActivationClip != null)
            audioSource.PlayOneShot(portalActivationClip);
        else
            Debug.LogWarning("Won game audio clip is not assigned in AudioManager.");
    }
}