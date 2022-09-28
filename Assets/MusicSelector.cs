using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour {

    public AudioSource[] tracks;
    public int size;
    public int trackSelector;
    public int trackHistory;



	// Use this for initialization
	void Start () {
        trackSelector = 0; //0
        trackHistory = trackSelector;
        tracks[trackSelector].Play();
	}
	


	// Update is called once per frame
	void Update () {
        if (tracks[trackSelector].isPlaying == false)
        {
            do
            {
                trackSelector = Random.Range(1, size);
            }
            while (trackSelector == trackHistory);

            trackHistory = trackSelector;
            tracks[trackSelector].Play();
        }
	}
}
