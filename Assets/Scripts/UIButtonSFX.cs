using UnityEngine;

public class UIButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    public void PlayClickSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clickSound);
        }
    }
}