using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoFeed : MonoBehaviour
{

    public Renderer renderer;
    public byte[] data;
    WebCamTexture webcamTexture;
    bool started = false;
    Texture2D currentFrame;
    IEnumerator openWebcam()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            webcamTexture = new WebCamTexture();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
            yield return new WaitForSeconds(1.0f);
            started = true;
        }
        else
        {
        }
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(openWebcam());
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (webcamTexture.didUpdateThisFrame)
            {
                currentFrame = new Texture2D(webcamTexture.width, webcamTexture.height);
                currentFrame.SetPixels(webcamTexture.GetPixels());
                currentFrame.Apply();
                data = currentFrame.EncodeToPNG();
            }
        }
    }

    public byte[] GetFramePNG()
    {
        return data;
    }
}
