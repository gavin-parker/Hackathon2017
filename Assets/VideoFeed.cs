﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class VideoFeed : MonoBehaviour
{

    public Renderer renderer;
    public byte[] data;
    WebCamTexture webcamTexture;
    bool started = false;
    Texture2D currentFrame;
    private ImageTextCapture _imageTextCapture;
    IEnumerator openWebcam()
    {
        yield return new WaitForSeconds(2.0f);
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {

           
            //Debug.Log(WebCamTexture.devices);
            
            WebCamDevice[] devices = WebCamTexture.devices;
            //Debug.Log(devices.Length);
            //webcamTexture = new WebCamTexture();

            if (devices.Length > 1)
            {
                webcamTexture = new WebCamTexture(devices[1].name);
                //Debug.Log("got here!");

            }
            else
            {
                Debug.Log("only one camera...soz...");
                //webcamTexture = new WebCamTexture(devices[0].name);
            }

            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();
            yield return new WaitForSeconds(0.5f);
            started = true;
        }
        else
        {
        }
    }
    // Use this for initialization
    void Start()
    {
        _imageTextCapture = new ImageTextCapture();
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
                
                List<String> text = _imageTextCapture.FindText(data);
                foreach (var words in text)
                {
                    Debug.Log(words);
                }
            }
        }
    }

    public byte[] GetFramePNG()
    {
        return data;
    }
}
