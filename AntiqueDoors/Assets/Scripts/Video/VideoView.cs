using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class VideoView : View
{
    [SerializeField] private VideoPlayers videoPlayers;

    public void Play(string id)
    {
        var videoPlayer = videoPlayers.GetVideoPlayerById(id);

        if (videoPlayer == null)
        {
            Debug.LogWarning($"VideoPlayer with id: {id} not found!");
            return;
        }

        videoPlayer.time = 0;
        videoPlayer.Play();
    }
}

[System.Serializable]
public class VideoPlayers
{
    [SerializeField] private List<VideoPlay> videoPlays = new();

    public VideoPlayer GetVideoPlayerById(string id) => videoPlays.FirstOrDefault(data => data.Id == id)?.VideoPlayer;
}

[System.Serializable]
public class VideoPlay
{
    [SerializeField] private string id;
    [SerializeField] private VideoPlayer videoPlayer;

    public string Id => id;
    public VideoPlayer VideoPlayer => videoPlayer;
}
