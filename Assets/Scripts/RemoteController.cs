using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUiKits.Utils;
using UnityEngine.Video;
using UnityEngine.UI;

public enum ItemLabel {
    Music,
    TV,
    PlayOrPause,
}

public class RemoteController : MonoBehaviour
{
    public GameObject ControllerMenu;
    RadialItem PlayOrPauseItem;
    RadialItem MusicItem;
    RadialItem TVItem;
    
    Image playOrPauseImage;
    Image musicImage;
    Image TVImage;

    public List<VideoClip> VideoClips;
    public List<VideoClip> MusicClips;
    public VideoPlayer PlayerObject;
    bool pause = true;
    bool music = true;
    int musicIdx = 0;
    int videoIdx = 0;

    Color activeColor = new Color(37f / 255f, 137f / 255f, 166f / 255f);
    Color inactiveColor = new Color(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GameObject playOrPauseObject = ControllerMenu.transform.Find("PlayOrPauseItem").gameObject;
        GameObject MusicObject = ControllerMenu.transform.Find("MusicItem").gameObject;
        GameObject TVObject = ControllerMenu.transform.Find("TVItem").gameObject;

        PlayOrPauseItem = playOrPauseObject.GetComponent<RadialItem>();
        MusicItem = MusicObject.GetComponent<RadialItem>();
        TVItem = TVObject.GetComponent<RadialItem>();

        playOrPauseImage = playOrPauseObject.transform.Find("Sector").gameObject.GetComponent<Image>();
        musicImage = MusicObject.transform.Find("Sector").gameObject.GetComponent<Image>();
        TVImage = TVObject.transform.Find("Sector").gameObject.GetComponent<Image>();
        
        SetColor(true, ItemLabel.Music);
    }

    void SetColor(bool active, ItemLabel itemLabel)
    {
        RadialItem item;
        Image itemImage;
        switch (itemLabel) {
            case ItemLabel.Music:
                item = MusicItem;
                itemImage = musicImage;
                break;
            case ItemLabel.TV:
                item = TVItem;
                itemImage = TVImage;
                break;
            case ItemLabel.PlayOrPause:
                item = PlayOrPauseItem;
                itemImage = playOrPauseImage;
                break;
            default:
                item = MusicItem;
                itemImage = musicImage;
                break;
        }
        if (active)
        {
            item.normalColor = activeColor;
            itemImage.color = activeColor;
        }
        else
        {
            item.normalColor = inactiveColor;
            itemImage.color = inactiveColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        Debug.Log("Play music");
        if (!music)
        {
            SetColor(false, ItemLabel.TV);
            SetColor(true, ItemLabel.Music);
            music = true;
        }
        if (!pause)
        {
            pausePlayer();
            playPlayer();
        }
    }

    public void PlayTV()
    {
        Debug.Log("Play TV");
        if (music)
        {
            SetColor(true, ItemLabel.TV);
            SetColor(false, ItemLabel.Music);
            music = false;
        }
        if (!pause)
        {
            pausePlayer();
            playPlayer();
        }
    }

    public void PlayOrPause()
    {
        Debug.Log("Play or pause");
        if (pause)
        {
            pause = false;
            SetColor(true, ItemLabel.PlayOrPause);
            playPlayer();
        }
        else
        {
            pause = true;
            SetColor(false, ItemLabel.PlayOrPause);
            pausePlayer();
        }
    }

    void pausePlayer()
    {
        PlayerObject.Pause();
    }

    void playPlayer()
    {
        if (music)
        {
            PlayerObject.clip = MusicClips[musicIdx];
        }
        else
        {
            PlayerObject.clip = VideoClips[videoIdx];
        }
        PlayerObject.Play();
    }

    public void NextVideo()
    {
        Debug.Log("Next video");
        if (music)
        {
            musicIdx ++;
            if (musicIdx == MusicClips.Count) musicIdx = 0;
        }
        else
        {
            videoIdx ++;
            if (videoIdx == VideoClips.Count) videoIdx = 0;
        }
        if (!pause)
        {
            pausePlayer();
            playPlayer();
        }
    }
}
