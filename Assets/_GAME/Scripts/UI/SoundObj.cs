using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundObj : MonoBehaviour
{
    public TypeSetting type;
    public Button btnSound;
    public GameObject on, off;
    public TMP_Text txtTitle;
    public bool isOn;
    string _title;


    private void Awake()
    {
        btnSound.onClick.AddListener(OnClickThis);
        UpdateTextByType();
    }

    private void OnEnable()
    {
        LoadData();
        UpdateUI();
    }

    void LoadData()
    {
        switch (type)
        {
            case TypeSetting.SOUND:
                isOn = PrefData.Sound;
                break;
            case TypeSetting.MUSIC:
                isOn = PrefData.Music;
                break;
            case TypeSetting.VIBRATE:
                isOn = PrefData.Vibrate;
                break;
        }
    }

    void SaveData()
    {
        switch (type)
        {
            case TypeSetting.SOUND:
                PrefData.Sound = isOn;
                break;
            case TypeSetting.MUSIC:
                PrefData.Music = isOn;
                SoundCtrl.I.CheckMusic();
                break;
            case TypeSetting.VIBRATE:
                PrefData.Vibrate = isOn;
                break;
        }
    }

    void OnClickThis()
    {
        isOn = !isOn;
        UpdateUI();
        SaveData();
    }

    void UpdateUI()
    {
        on.SetActive(isOn);
        off.SetActive(!isOn);
    }

    void UpdateTextByType()
    {
        switch (type)
        {
            case TypeSetting.SOUND:
                _title = "SOUND";
                break;
            case TypeSetting.MUSIC:
                _title = "MUSIC";
                break;
            case TypeSetting.VIBRATE:
                _title = "VIBRATE";
                break;
        }
        txtTitle.text = _title;
    }

    public enum TypeSetting { SOUND, MUSIC, VIBRATE}   
}
