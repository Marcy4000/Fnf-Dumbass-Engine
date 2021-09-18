using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CreateSectionRight : MonoBehaviour
{

    public List<GameObject> TogglesObject;
    public List<bool> Values;
    public List<int> HoldTime;
    [SerializeField] SongJsonnnn song = new SongJsonnnn();

    Exported exported;

    public int SectionId = 0;
    public SectionId id;


    //create a list of the toggles, kinda useless. just needed fot the update list method
    void Start()
    {
        string json = GlobalDataSfutt.ReadShit(GlobalDataSfutt.songNameToLoad);
        JsonUtility.FromJsonOverwrite(json, song);

        SectionId = id.currentSectionId;
        
        foreach (Transform child in transform)
        { 
            TogglesObject.Add(child.gameObject);
        }
        StartCoroutine(DoTheLoading());

        
    }
    //updates chart list with booleans, gonna change this soon
    public void UpdateList()
    {
        Values.Clear();
        
        foreach (GameObject Value in TogglesObject)
        {
            
            Toggle thing;

            thing = Value.GetComponent<Toggle>();
            Values.Add(thing.isOn);
            HoldTime.Add(thing.GetComponent<ToggleHoldNote>().HoldNoteTime);

        }
    }

    IEnumerator DoTheLoading()
    {
        yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i < 16; i++)
        {
            Toggle thing;
            GameObject stuff;
            int coolName;
            coolName = i + (16 * SectionId);
            stuff = TogglesObject.ElementAt(i);
            thing = stuff.GetComponent<Toggle>();
            thing.isOn = song.notesRight[coolName];
        }
        UpdateList();
    }

    public void ClearSection()
    {
        for (int i = 0; i < 16; i++)
        {
            Toggle thing;
            GameObject stuff;
            int coolName;
            coolName = i + (16 * SectionId);
            stuff = TogglesObject.ElementAt(i);
            thing = stuff.GetComponent<Toggle>();
            thing.isOn = false;
        }
        UpdateList();
    }

}

[System.Serializable]
public class SongJsonnnn
{
    //json values
    public string songName = "DefaultSusName";
    public int bpm = 150;
    public float scrollSpeed = 7f;

    //probally i should not use booleans, but i'm too lazy to change
    public bool[] notesLeft;
    public bool[] notesDown;
    public bool[] notesUp;
    public bool[] notesRight;

    public int[] holdNotesLeft;
    public int[] holdNotesDown;
    public int[] holdNotesUp;
    public int[] holdNotesRight;
}