using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;

public class cleanText : MonoBehaviour {
    
    string[] stopwords = {"i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours",
"yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers",
"herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves",
"what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are",
"was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does",
"did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until",
"while", "of", "at", "by", "for", "with", "about", "against", "between", "into",
"through", "during", "before", "after", "above", "below", "to", "from", "up", "down",
"in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here",
"there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more",
"most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so",
"than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now" };

    //public string Trim(string textToConvert){

    //    //string textToConvert = "The duck crossed the four lane road.";
    //    string result = System.Text.RegularExpressions.Regex.Replace(textToConvert.ToLower(), @"(([-]|[.]|[-.]|[0-9])?[0-9]*([.]|[,])*[0-9]+)|(\b\w{1,2}\b)|([^\w])", " ");
    //    Debug.Log(result.ToString());

    //    StringBuilder inText = new StringBuilder(" " + result + " ");
    //    foreach (string word in stopwords)
    //    {
    //        inText.Replace(" " + word + " ", " ");
    //    }
    //    Debug.Log(inText.ToString());

    //    return (inText.ToString());

    //}
    void Start(){
        run_cmd("The quick brown fox jumps over the lazy dog.");

    }
    private void run_cmd(string text)
    {
        //string fileName = "/Hackathon2017/Assets/getNouns.py";
        UnityEngine.Debug.Log(Environment.CurrentDirectory);

        string path = Path.Combine(Environment.CurrentDirectory, @"/Assets/getNouns.py");
        //string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
        string[] args = { path, text };
        string cmd = "/usr/local/bin/python"; 

        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = cmd;
        start.Arguments = string.Format("{0} {1}", cmd, args);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;

        Process process = Process.Start(start);
        UnityEngine.Debug.Log("Process started yeyy");
        UnityEngine.Debug.Log("Reading output");
        string result = process.StandardOutput.ReadToEnd();
        string err = process.StandardError.ReadToEnd();
        //process.OutputDataReceived += (sender, e) => { if (e != null) UnityEngine.Debug.Log(e.Data); };
        //process.BeginOutputReadLine();
        process.WaitForExit();
        UnityEngine.Debug.Log(result);
        UnityEngine.Debug.Log(err);


    }
}
