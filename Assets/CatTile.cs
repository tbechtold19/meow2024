using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Windows.Forms;

public class CatTile : MonoBehaviour
{
     public string noteName;
     public string key;

     public AudioClip audioClip;

     public Sprite originalSprite;
     public Sprite pressedSprite;
    private SpriteRenderer spriteRenderer;

    public PianoGame pianoGame;

     private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pianoGame = FindObjectOfType<PianoGame>(); // Find the PianoGame script in the scene

    }

    private void Update()
    {
        // Check for key press corresponding to the piano key
        if (Input.GetKeyDown(key))
        {

            StartCoroutine(PlayAndResetSprite());

            // Check if the PianoGame is running before calling OnPianoKeyPressed
            if (pianoGame != null && pianoGame.IsGameRunning())
            {
                pianoGame.OnPianoKeyPressed(noteName);
            }

        }
        
    }

    private void OnMouseDown()
    {
        StartCoroutine(PlayAndResetSprite());

        // Check if the PianoGame is running before calling OnPianoKeyPressed
        if (pianoGame != null && pianoGame.IsGameRunning())
        {
            pianoGame.OnPianoKeyPressed(noteName);
        }

        //audioSource.clip = audioClip;
        //spriteRenderer.sprite = pressedSprite;
        //audioSource.Play();
        //wait(3000);
        //spriteRenderer.sprite = originalSprite;

    }

 
    public IEnumerator PlayAndResetSprite()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        spriteRenderer.sprite = pressedSprite;
        audioSource.PlayOneShot(audioClip, 0.7F);

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
