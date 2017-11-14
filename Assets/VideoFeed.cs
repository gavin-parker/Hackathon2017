using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoFeed : MonoBehaviour {

    public Renderer renderer;

    IEnumerator openWebcam()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamTexture webcamTexture = new WebCamTexture();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(openWebcam());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
