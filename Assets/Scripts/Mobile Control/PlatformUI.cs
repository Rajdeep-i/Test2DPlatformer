using UnityEngine;

public class PlatformUI : MonoBehaviour
{
    private void Start()
    {
          #if UNITY_ANDROID || UNITY_IOS
            gameObject.SetActive(true);
        #else
             gameObject.SetActive(false);
        #endif
    }
}