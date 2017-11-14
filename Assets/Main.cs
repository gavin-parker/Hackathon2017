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
            int i = 0;
            List<BoundingPoly> polys = feed._imageTextCapture.boundPoly;
            List<string> words = feed._imageTextCapture.readList;
            foreach (string word in words)
            {
                images[position_index].gameObject.SetActive(true);
                images[position_index].setImage(word);
                images[position_index].this_string = word;
                Vector3 screenPos = feed.renderer.gameObject.transform.position;
                Vector3 bottomleft = screenPos;
                bottomleft.x = screenPos.x - feed.renderer.gameObject.transform.localScale.x / 2;
                bottomleft.y = screenPos.y - feed.renderer.gameObject.transform.localScale.y / 2;

                Debug.DrawLine(images[position_index].gameObject.transform.position, new Vector3(1, 0, 0), Color.red);
                position_index = (position_index + 1) % images.Count;
                i++;
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
