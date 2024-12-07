using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHelper : MonoBehaviour
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer videoPlayer = image.GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "FinalIntroVideo.mp4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
