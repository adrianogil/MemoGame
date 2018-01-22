using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ImageData
{
    public string name;
    public Texture2D texture;
}


public class ImageManager : MonoBehaviour {

    public List<ImageData> images;

	// Use this for initialization
	void Start () {
	
        SaveImagesInto();

	}
	
	void SaveImagesInto () {

        Debug.Log("GilLog - ImageManager::SaveImagesInto - "  + Application.persistentDataPath);

        byte[] imageBytes;
        string imagePath = "";

        for (int i = 0; i < images.Count; i++)
        {
            // Save Texture2D into file
            imageBytes = images[i].texture.EncodeToPNG();

            imagePath = Application.persistentDataPath + "/images/" + images[i].name + ".png";
            File.WriteAllBytes(imagePath, imageBytes);
            Debug.Log("GilLog - ImageManager::SaveImagesInto - Saved image " + imagePath);
        }
	}
}
