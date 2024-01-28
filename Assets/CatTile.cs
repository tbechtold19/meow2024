using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Windows.Forms;

public class CatTile : MonoBehaviour
{
     public string noteName;
     public AudioClip audioClip;

     public Sprite originalSprite;
     public Sprite pressedSprite;
    private SpriteRenderer spriteRenderer;

     private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnMouseDown()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(PlayAndResetSprite(audioSource));

        //audioSource.clip = audioClip;
        //spriteRenderer.sprite = pressedSprite;
        //audioSource.Play();
        //wait(3000);
        //spriteRenderer.sprite = originalSprite;

    }

 
    IEnumerator PlayAndResetSprite(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        spriteRenderer.sprite = pressedSprite;
        audioSource.Play();

        // Wait for the next frame without freezing the UI
        yield return null;

        // Wait until the audio has finished playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Reset to the original sprite after audio has finished playing
        spriteRenderer.sprite = originalSprite;
    }


    /*
    public void wait(int milliseconds)
    {
        var timer1 = new System.Windows.Forms.Timer();
        if (milliseconds == 0 || milliseconds < 0) return;

        // Console.WriteLine("start wait timer");
        timer1.Interval = milliseconds;
        timer1.Enabled  = true;
        timer1.Start();

        timer1.Tick += (s, e) =>
        {
            timer1.Enabled = false;
            timer1.Stop();
            // Console.WriteLine("stop wait timer");
        };

        while (timer1.Enabled)
        {
            Application.DoEvents();
        }
    } 
    */

}
