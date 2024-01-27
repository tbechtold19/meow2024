using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Keyboard : MonoBehaviour
{

    public GameObject catTile, kittenTile;

    public GameObject content;

    public int numberOfOctaves;

    // Start is called before the first frame update
    void Start()
    {
        int startNote = 12;
        
        for (int i = 0; i < numberOfOctaves; i++)
        {

            createOctave(startNote + i * 12, i);

        }
        
    }

    private void createOctave(int startNote, int octave) 
    {

        float width = content.GetComponent<RectTransform>().rect.width;
        float widthPerOctave = width / numberOfOctaves;
        float widthPerNote = widthPerOctave / 7;

        for (int i = 0; i < 7; i++)
        {

            int actualNoteIndex = getCatKeyIndex(i);
            GameObject note = instantiateNote(kittenTile, actualNoteIndex, startNote); 
            registerEvents(note);

            note.GetComponent<RectTransform>().sizeDelta = new Vector2(widthPerNote/2, note.GetComponent<RectTransform>().sizeDelta.y);

            int kittenIndex = i; 
            
            if (i > 1) {

                kittenIndex += 1;

            }

            note.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(widthPerOctave*octave+widthPerNote*kittenIndex+widthPerNote, -kittenTile.GetComponent<RectTransform>().rect.height/2, 0);

        }

    }

    private void registerEvents(GameObject note)
    {
        EventTrigger trigger = note.gameObject.AddComponent<EventTrigger>(); 
        var pointerDown = new EventTrigger.Entry(); 
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => keyOn(note.GetComponent<CatTile>().midiNote));
        trigger.triggers.Add(pointerDown); 

        var pointerUp = new EventTrigger.Entry(); 
        pointerUp.eventID = EventTriggerType.PointerUp; 
        pointerUp.callback.AddListener((e) => keyOff(note.GetComponent<CatTile>().midiNote)); 
        trigger.triggers.Add(pointerUp); 
    }

    public void keyOn(int midiNumber)
    {
        Debug.Log("Clicked " + midiNumber); 
        GameObject.Find("SoundGen").GetComponent<SoundGen>().OnKey(midiNumber);
    }

    public void keyOff(int midiNumber)
    {

        Debug.Log("Released " + midiNumber);
        GameObject.Find("SoundGen").GetComponent<SoundGen>().onKeyOff(midiNumber);

    }

    private GameObject instantiateNote(GameObject note, int actualNoteIndex, int startNote)
    {

        GameObject newNote = Instantiate(note);
        newNote.transform.SetParent(content.transform, false);
        newNote.GetComponent<CatTile>().midiNote = startNote + actualNoteIndex;
        return newNote;

    }


    private int getCatKeyIndex(int i)
    {
        // C
        int actualNoteIndex = 0;

        if (i == 1) {

            // D
            actualNoteIndex = 2;

        } else if (i == 2) {

            //E
            actualNoteIndex = 4;

        } else if (i == 3) {

            //F
            actualNoteIndex = 5;

        } else if (i == 4) {

            //G
            actualNoteIndex = 7;

        } else if (i == 5) {

            //A
            actualNoteIndex = 9;

        } else if(i == 6){

            //A
            actualNoteIndex = 9;

        }

        return actualNoteIndex;

    }

    private int getKittenKeyIndex(int i) {

        // CS
        int actualNote = 1; 
        
        if (i == 1) {
            
            // DS
            actualNote = 3;

        } else if (i == 2) {
            
            // ES
            actualNote = 6;

        } else if (i == 3) {

            //FS
            actualNote = 8;

        }

        return actualNote;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}