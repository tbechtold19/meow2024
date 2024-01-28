using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneTransitionOnTimelineEnd : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Start()
    {
        // Get the PlayableDirector component on this GameObject
        playableDirector = GetComponent<PlayableDirector>();

        // Subscribe to the Director's "stopped" event
        playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Check if the Director that stopped is the one we're interested in
        if (director == playableDirector)
        {
            // Load the next scene when the timeline has finished playing
            SceneManager.LoadScene(1);
        }
    }
}
