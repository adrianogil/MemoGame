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

    public static ImageManager Instance
    {
        get;
        private set;
    }

    public const int MAX_ITEMS = 9;
    private const string IMG_ALRDY_SAVED_KEY = "IMG_ALRDY_SAVED_KEY";

    public List<ImageData> images;

    void Awake()
    {
        Instance = this;

        if (PlayerPrefs.GetInt(IMG_ALRDY_SAVED_KEY, 0) == 0)
        {
            SaveDefaultImages();
        }
    }

    public void ShuffleImages()
    {
        for (int i = 0; i < MAX_ITEMS && i < images.Count; i++)
        {
            images[i] = images[Random.Range(0, images.Count)];
        }
    }
	
	void SaveDefaultImages() {

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

            PlayerPrefs.SetString("MEMO_IMAGES_" + i, imagePath);
        }

        PlayerPrefs.SetInt(IMG_ALRDY_SAVED_KEY, 1);
	}
}
