using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject position_parent;
    public ImageSearch floating_image;
    List<Transform> positions;
    int position_index = 0;

    string[] demoWords = { "cat", "dog", "potato", "pizza", "car", "fish" };

    IEnumerator addImage()
    {
        foreach(string word in demoWords)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject newImage = Instantiate(floating_image.gameObject);
            newImage.GetComponent<ImageSearch>().setImage(word);
            newImage.transform.position = positions[position_index].position;
            position_index = (position_index + 1) % positions.Count;
        }
    }


	// Use this for initialization
	void Start () {
        positions = new List<Transform>();
        for(int i=0; i < position_parent.transform.childCount; i++)
        {
            positions.Add(position_parent.transform.GetChild(i));
        }
        StartCoroutine(addImage());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
