using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageSearch : MonoBehaviour
{

    private string api = "https://pixabay.com/api/?key=7045395-a2d27739a618f360c06fb3c3f&q=";

    public void setImage(string search_term)
    {
        StartCoroutine(getImages(search_term));
    }


    IEnumerator getImages(string term)
    {
        UnityWebRequest www = UnityWebRequest.Get(api + term);
        yield return www.SendWebRequest();  

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error.ToString());

        }
        else
        {
            // Show results as text
            ImageResponse response = JsonUtility.FromJson<ImageResponse>(www.downloadHandler.text);
            StartCoroutine(getImageData(response.hits[0].webformatURL, response.hits[0].webFormatWidth, response.hits[0].webFormatHeight));
        }
    }

    IEnumerator getImageData(string url, int width, int height)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error.ToString());

        }
        else
        {
            // Show results as text
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            Texture2D image = new Texture2D(width, height);
            image.LoadImage(results);
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = image;
        }
    }
}
