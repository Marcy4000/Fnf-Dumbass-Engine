using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown stageDropdown;
    public Slider volumeSlider;
    public Toggle ghostTapping;
    private bool settingKeybind;

    [Header("Keybinding")] public TMP_Text primaryLeftKeybindText;
    public TMP_Text primaryDownKeybindText;
    public TMP_Text primaryUpKeybindText;
    public TMP_Text primaryRightKeybindText;
    public TMP_Text secondaryLeftKeybindText;
    public TMP_Text secondaryDownKeybindText;
    public TMP_Text secondaryUpKeybindText;
    public TMP_Text secondaryRightKeybindText;
    public TMP_Text pauseKeybindText;
    public TMP_Text resetKeybindText;
    private KeybindSet _currentKeybindSet;

    private void Start()
    {
        stageDropdown.value = GlobalDataSfutt.selectedStage;
        volumeSlider.value = AudioListener.volume;
        ghostTapping.isOn = GlobalDataSfutt.ghostTapping;

        primaryLeftKeybindText.text = "LEFT\n" + Player.leftArrowKey;
        primaryDownKeybindText.text = "DOWN\n" + Player.downArrowKey;
        primaryUpKeybindText.text = "UP\n" + Player.upArrowKey;
        primaryRightKeybindText.text = "RIGHT\n" + Player.rightArrowKey;
        secondaryLeftKeybindText.text = "LEFT\n" + Player.secLeftArrowKey;
        secondaryDownKeybindText.text = "DOWN\n" + Player.secDownArrowKey;
        secondaryUpKeybindText.text = "UP\n" + Player.secUpArrowKey;
        secondaryRightKeybindText.text = "RIGHT\n" + Player.secRightArrowKey;
        pauseKeybindText.text = "PAUSE\n" + Player.pauseKey;
        resetKeybindText.text = "RESET\n" + Player.resetKey;
    }

    private void Update()
    {
        if (settingKeybind)
        {
            if (!Input.anyKeyDown) return;

            KeyCode newKey = KeyCode.A;

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    newKey = kcode;
                    break;
                }
            }

            switch (_currentKeybindSet)
            {
                case KeybindSet.PrimaryLeft:
                    primaryLeftKeybindText.text = "LEFT\n" + newKey;
                    Player.leftArrowKey = newKey;
                    break;
                case KeybindSet.PrimaryDown:
                    primaryDownKeybindText.text = "DOWN\n" + newKey;
                    Player.downArrowKey = newKey;
                    break;
                case KeybindSet.PrimaryUp:
                    primaryUpKeybindText.text = "UP\n" + newKey;
                    Player.upArrowKey = newKey;
                    break;
                case KeybindSet.PrimaryRight:
                    primaryRightKeybindText.text = "RIGHT\n" + newKey;
                    Player.rightArrowKey = newKey;
                    break;
                case KeybindSet.SecondaryLeft:
                    secondaryLeftKeybindText.text = "LEFT\n" + newKey;
                    Player.secLeftArrowKey = newKey;
                    break;
                case KeybindSet.SecondaryDown:
                    secondaryDownKeybindText.text = "DOWN\n" + newKey;
                    Player.secDownArrowKey = newKey;
                    break;
                case KeybindSet.SecondaryUp:
                    secondaryUpKeybindText.text = "UP\n" + newKey;
                    Player.secUpArrowKey = newKey;
                    break;
                case KeybindSet.SecondaryRight:
                    secondaryRightKeybindText.text = "RIGHT\n" + newKey;
                    Player.secRightArrowKey = newKey;
                    break;
                case KeybindSet.Pause:
                    pauseKeybindText.text = "PAUSE\n" + newKey;
                    Player.pauseKey = newKey;
                    break;
                case KeybindSet.Reset:
                    resetKeybindText.text = "RESET\n" + newKey;
                    Player.resetKey = newKey;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Player.SaveKeySet();
            settingKeybind = false;
        }
    }

    public void SetGhostTapping()
    {
        GlobalDataSfutt.ghostTapping = ghostTapping.isOn;
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SetStage()
    {
        GlobalDataSfutt.selectedStage = stageDropdown.value;
    }

    public enum KeybindSet
    {
        PrimaryLeft = 1,
        PrimaryDown = 2,
        PrimaryUp = 3,
        PrimaryRight = 4,
        SecondaryLeft = 5,
        SecondaryDown = 6,
        SecondaryUp = 7,
        SecondaryRight = 8,
        Pause = 9,
        Reset = 10
    }

    public void ChangeKeybind(int key)
    {
        KeybindSet keybind = (KeybindSet)Enum.ToObject(typeof(KeybindSet), key);

        _currentKeybindSet = keybind;
        settingKeybind = true;

        switch (keybind)
        {
            case KeybindSet.PrimaryLeft:
                primaryLeftKeybindText.text = "LEFT\nPress a Key";
                break;
            case KeybindSet.PrimaryDown:
                primaryDownKeybindText.text = "DOWN\nPress a Key";
                break;
            case KeybindSet.PrimaryUp:
                primaryUpKeybindText.text = "UP\nPress a Key";
                break;
            case KeybindSet.PrimaryRight:
                primaryRightKeybindText.text = "RIGHT\nPress a Key";
                break;
            case KeybindSet.SecondaryLeft:
                secondaryLeftKeybindText.text = "LEFT\nPress a Key";
                break;
            case KeybindSet.SecondaryDown:
                secondaryDownKeybindText.text = "DOWN\nPress a Key";
                break;
            case KeybindSet.SecondaryUp:
                secondaryUpKeybindText.text = "UP\nPress a Key";
                break;
            case KeybindSet.SecondaryRight:
                secondaryRightKeybindText.text = "RIGHT\nPress a Key";
                break;
            case KeybindSet.Pause:
                pauseKeybindText.text = "PAUSE\nPress a Key";
                break;
            case KeybindSet.Reset:
                resetKeybindText.text = "RESET\nPress a Key";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
