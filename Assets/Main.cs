using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject position_parent;
    public ImageSearch floating_image;
    List<Transform> positions;
    List<ImageSearch> images;
    public VideoFeed feed;
    int position_index = 0;

    string[] demoWords = { "cat", "dog", "potato", "pizza", "car", "fish" };

    IEnumerator addImage()
    {
        while(true)
        {
            yield return new WaitForSeconds(5.0f);
            List<string> words = feed._imageTextCapture.readList;
            foreach(string word in words)
            {
                images[position_index].gameObject.SetActive(true);
                images[position_index].setImage(word);
                position_index = (position_index + 1) % images.Count;
            }

            
        }
    }
    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        positions = new List<Transform>();
        images = new List<ImageSearch>();
        for(int i=0; i < position_parent.transform.childCount; i++)
        {
            positions.Add(position_parent.transform.GetChild(i));
            GameObject newImage = Instantiate(floating_image.gameObject);
            newImage.SetActive(false);
            newImage.transform.position = position_parent.transform.GetChild(i).position;
            images.Add(newImage.GetComponent<ImageSearch>());
        }

        StartCoroutine(addImage());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
