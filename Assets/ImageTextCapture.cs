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
       
        void FinishWebRequest(IAsyncResult result)
        {
            webRequest.EndGetResponse(result);
        }
        
        public List<String> FindText(byte[] imageBytes)
        {

            String base64String = Convert.ToBase64String(imageBytes);
            
            string endpoint =
                "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyDhX1jANfIk5IFxVtywrncpUCpwKKbcsHw";

            String body = "{\"requests\":[{\"image\":{\"content\": \" " + base64String +
                          "\" },\"features\":[{\"type\":\"TEXT_DETECTION\"}]}]}";
            
            byte[] data = Encoding.ASCII.GetBytes(body);
            IgnoreBadCertificates();
            WebRequest request = WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            
            Stream stream1 = request.GetRequestStream();
            stream1.Write(data, 0, data.Length);
            stream1.Flush();
            stream1.Close();

            string responseContent = null;
            
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr99 = new StreamReader(stream))
                    {
                        responseContent = sr99.ReadToEnd();
                    }
                }
            }
            
            RootObject rootObject =  JsonConvert.DeserializeObject<RootObject>(responseContent);
            
            return rootObject.print();

        }
        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        /// <summary>
        /// In Short: the Method solves the Problem of broken Certificates.
        /// Sometime when requesting Data and the sending Webserverconnection
        /// is based on a SSL Connection, an Error is caused by Servers whoes
        /// Certificate(s) have Errors. Like when the Cert is out of date
        /// and much more... So at this point when calling the method,
        /// this behaviour is prevented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>true</returns>
        private static bool AcceptAllCertifications(object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;

        }
        


    }
            
            
            
            
            
            /*Image image = Image.FromBytes(imageBytes);
            
            // Instantiates a client
            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            // Performs label detection on the image file
            var response = client.DetectText(image);
            List<String> result = new List<String>();
            
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    result.Add(annotation.Description);
                }
            }
            return result;*/
            
    
}