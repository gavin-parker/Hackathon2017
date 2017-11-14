using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    
	// Use this for initialization
    void Start () {

        //static string inText = "The duck crossed the four lane road.";

        //string[] output = string.Join(" ", inText.Split(' ').Where(wrd => !stopwords.Contains(wrd)));
        string textToConvert = "The duck crossed the four lane road.";
        string result = System.Text.RegularExpressions.Regex.Replace(textToConvert.ToLower(), @"(([-]|[.]|[-.]|[0-9])?[0-9]*([.]|[,])*[0-9]+)|(\b\w{1,2}\b)|([^\w])", " ");
        Debug.Log(result.ToString());

        StringBuilder inText = new StringBuilder(" "+result+" ");
        foreach (string word in stopwords){
                inText.Replace(" " + word + " ", " ");
        }
        Debug.Log(inText.ToString());
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
