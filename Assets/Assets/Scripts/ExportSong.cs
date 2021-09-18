using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ExportSong : MonoBehaviour
{
    [HideInInspector]public SectionMenager menager;


    public TMP_InputField songName;
    public TMP_InputField bpm;
    public TMP_InputField scrollSpeed;
    [SerializeField] Exporting song = new Exporting(); //idk what this is but it works so...
    public GameObject[] leftSection;
    public GameObject[] downSection;
    public GameObject[] upSection;
    public GameObject[] rightSection;

    public List<bool> leftFullChart;
    public List<bool> downFullChart;
    public List<bool> upFullChart;
    public List<bool> rightFullChart;

    public List<int> leftFullHold;
    public List<int> downFullHold;
    public List<int> upFullHold;
    public List<int> rightFullHold;


    void Start()
    {
        /*
        //getting note list from section (old)
        GameObject sectionLeft = GameObject.Find("SectionLeft");//Left
        noteListLeft = sectionLeft.GetComponent<CreateSection>();
        //down
        GameObject sectionDown = GameObject.Find("SectionDown");
        noteListDown = sectionDown.GetComponent<CreateSection>();
        //Up
        GameObject sectionUp = GameObject.Find("SectionUp");
        noteListUp = sectionUp.GetComponent<CreateSection>();
        //Right
        GameObject sectionRight = GameObject.Find("SectionRight");
        noteListRight = sectionRight.GetComponent<CreateSection>();
        */
        GameObject manager = GameObject.Find("SectionMenu");
        menager = manager.GetComponent<SectionMenager>();

        
    }

    private void Update()
    {
        //stuff for scene stuff, idk why i put those here
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    public void Export()
    {
        //making sure the player doesn't just change shit while saving
        songName.readOnly = true;
        bpm.readOnly = true;
        scrollSpeed.readOnly = true;

        

        //Setting correct values to save
        song.songName = songName.text;
        song.bpm = int.Parse(bpm.text);
        song.scrollSpeed = float.Parse(scrollSpeed.text);
        /*
         * song.notesLeft = noteListLeft.Values.ToArray();
        song.notesDown = noteListDown.Values.ToArray();
        song.notesUp = noteListUp.Values.ToArray();
        song.notesRight = noteListRight.Values.ToArray();
        */
        for (int i = 0; i < menager.highestSectionIdSelected + 1; i++)
        {
            CreateSectionLeft section;
            List<bool> currentChart;
            List<int> currentHoldTimes;
            section = leftSection[i].GetComponent<CreateSectionLeft>();
            
            currentChart = section.Values;
            currentHoldTimes = section.HoldTime;
            //leftFullChart.AddRange(currentChart);
            currentChart.ForEach(item => leftFullChart.Add(item));
            currentHoldTimes.ForEach(item => leftFullHold.Add(item));
        }
        for (int l = 0; l < menager.highestSectionIdSelected + 1; l++)
        {
            CreateSectionDown section;
            List<bool> currentChart;
            List<int> currentHoldTimes;
            section = downSection[l].GetComponent<CreateSectionDown>();

            currentChart = section.Values;
            currentHoldTimes = section.HoldTime;
            //downFullChart.AddRange(currentChart);
            currentChart.ForEach(item => downFullChart.Add(item));
            currentHoldTimes.ForEach(item => downFullHold.Add(item));
        }
        for (int k = 0; k < menager.highestSectionIdSelected + 1; k++)
        {
            CreateSectionUp section;
            List<bool> currentChart;
            List<int> currentHoldTimes;
            section = upSection[k].GetComponent<CreateSectionUp>();

            currentChart = section.Values;
            currentHoldTimes = section.HoldTime;
            //upFullChart.AddRange(currentChart);
            currentChart.ForEach(item => upFullChart.Add(item));
            currentHoldTimes.ForEach(item => upFullHold.Add(item));
        }
        for (int s = 0; s < menager.highestSectionIdSelected + 1; s++)
        {
            CreateSectionRight section;
            List<bool> currentChart;
            List<int> currentHoldTimes;
            section = rightSection[s].GetComponent<CreateSectionRight>();

            currentChart = section.Values;
            currentHoldTimes = section.HoldTime;
            //rightFullChart.AddRange(currentChart);
            currentChart.ForEach(item => rightFullChart.Add(item));
            currentHoldTimes.ForEach(item => rightFullHold.Add(item));

        }    
        song.notesLeft = leftFullChart.ToArray();
        song.notesDown = downFullChart.ToArray();
        song.notesUp = upFullChart.ToArray();
        song.notesRight = rightFullChart.ToArray();

        song.holdNotesLeft = leftFullHold.ToArray();
        song.holdNotesDown = downFullHold.ToArray();
        song.holdNotesUp = upFullHold.ToArray();
        song.holdNotesRight = rightFullHold.ToArray();


        //if song name is empty just default to name
        if (song.bpm == 0)
        {
            song.bpm = 100;
        }
        if (song.songName == null || song.songName == "")
        {
            song.songName = "DefaultSusName";
        }

        //idk what this shit does, just googled it lol
        string chart = JsonUtility.ToJson(song);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + song.songName + ".json", chart);
        Debug.Log("this shit was saved in " + Application.persistentDataPath);

        songName.readOnly = false;
        bpm.readOnly = false;
        scrollSpeed.readOnly = false;
        
    }
}

[System.Serializable]
public class Exporting
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