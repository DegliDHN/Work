using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayPauseVideo : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    void Start(){
        videoPlayer = GetComponent<VideoPlayer>();        

    }

    // Start is called before the first frame update
    public void PlayPause()
    {
        if(videoPlayer.isPlaying){
            videoPlayer.Pause();
        } else {
            videoPlayer.Play();
        }
    }

}
