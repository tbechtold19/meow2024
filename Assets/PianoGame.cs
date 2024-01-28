using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoGame : MonoBehaviour
{

    public CatTile[] catTiles; // Reference to all the piano keys
    public int sequenceLength = 3; // Number of notes in the sequence
    public float delayBetweenNotes = 1f; // Time between notes in the sequence
     public float delayBeforeNewGame = 2f;
    public bool isRunning = false;

    private Coroutine gameCoroutine;
    private string[] sequence;
    private int currentIndex;

    private void OnMouseDown()
    {
        StartGame();

    }

    private void StartGame()
    {
        sequence = new string[sequenceLength];
        currentIndex = 0;

        isRunning =  true;
        // Generate a random sequence of notes
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence[i] = catTiles[Random.Range(0, catTiles.Length - 1)].noteName;
        }

        // Start playing the sequence
        gameCoroutine = StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        foreach (var note in sequence)
        {
            // Highlight the key
            foreach (var key in catTiles)
            {
                if (key.noteName == note)
                {
                    StartCoroutine(key.PlayAndResetSprite());
                    yield return new WaitForSeconds(delayBetweenNotes);
                }
            }
        }

        // Allow player input after the sequence is played
        currentIndex = 0;
    }

    private void CheckPlayerInput(string inputNote)
    {
        if (inputNote == sequence[currentIndex])
        {
            // Correct note played
            currentIndex++;

            if (currentIndex == sequence.Length)
            {
                // Player successfully played the entire sequence
                Debug.Log("Sequence completed! Starting a new one.");
                StartGame();
            }
        }
        else
        {
            // Incorrect note played, you may want to handle this differently (e.g., end the game)
            Debug.Log("Incorrect note. Try again.");
            currentIndex = 0;
            // Optionally, you might want to give the player a chance to try again or end the game.
        }
    }

    public void OnPianoKeyPressed(string noteName)
    {
        CheckPlayerInput(noteName);
    }

    public bool IsGameRunning() 
    {

        return isRunning;

    }

}
