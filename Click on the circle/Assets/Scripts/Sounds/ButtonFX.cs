using UnityEngine;

public class ButtonFX : MonoBehaviour
{
   [SerializeField] private AudioSource myFx;
   [SerializeField] private AudioClip clickFx;

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
