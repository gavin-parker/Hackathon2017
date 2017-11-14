using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

//using Google.Cloud.Vision.V1;


namespace DefaultNamespace
{
    public class ImageTextCapture
    {
        HttpWebRequest webRequest;
        public List<String> readList = new List<string>();
        public List<BoundingPoly> boundPoly = new List<BoundingPoly>();


        public IEnumerator FindText(byte[] imageBytes)
        {

            String base64String = Convert.ToBase64String(imageBytes);

            string endpoint =
                "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyDhX1jANfIk5IFxVtywrncpUCpwKKbcsHw";

            String body = "{\"requests\":[{\"image\":{\"content\": \" " + base64String +
                          "\" },\"features\":[{\"type\":\"TEXT_DETECTION\"}]}]}";

            byte[] data = Encoding.ASCII.GetBytes(body);

            //IgnoreBadCertificates();



            /*WebRequest request = WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            
            Stream stream1 = request.GetRequestStream();
            stream1.Write(data, 0, data.Length);
            stream1.Flush();
            stream1.Close();

            string responseContent = null;*/



            UnityWebRequest requestU = new UnityWebRequest(endpoint, UnityWebRequest.kHttpVerbPOST);
            requestU.uploadHandler = (UploadHandler) new UploadHandlerRaw(data);
            requestU.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            requestU.SetRequestHeader("Content-Type", "application/json");

            yield return requestU.SendWebRequest();

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(requestU.downloadHandler.text);

            if (rootObject != null)
            {
                readList.Clear();
                boundPoly.Clear();
                rootObject.print(readList, boundPoly);
            }
        }
    }
}