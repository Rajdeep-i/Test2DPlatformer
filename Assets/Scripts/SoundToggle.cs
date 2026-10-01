using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;

    private bool isMuted = false;

    public void ToggleSound()
    {
        isMuted = !isMuted;

        AudioListener.volume = isMuted ? 0f : 1f;

        soundOn.SetActive(!isMuted);
        soundOff.SetActive(isMuted);
    }
}