using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotPreview : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    string[] files = null;
    int whichScreenShotIsShown = 0;

    // Start is called before the first frame update
    void Start()
    {
        //files = Directory.GetFiles(Application.dataPath + "/", "*.png");
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        Debug.Log(Application.dataPath);
        Debug.Log(files.Length);
        if (files.Length > 0)
        {
            Debug.Log("불러옴");
            GetPictureAndShowIt();
        }
    }

    void GetPictureAndShowIt()
    {
        string pathToFile = files[whichScreenShotIsShown];
        Debug.Log(pathToFile);
        Texture2D texture = GetScreenshotImage(pathToFile);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        canvas.GetComponent<Image>().sprite = sp; 
    }

    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    public void NextPicture()
    {
        if (files.Length > 0)
        {
            whichScreenShotIsShown += 1;
            if (whichScreenShotIsShown > files.Length - 1)
                whichScreenShotIsShown = 0;
            GetPictureAndShowIt();
        }
    }

    public void PreviousPicture()
    {
        if (files.Length > 0)
        {
            whichScreenShotIsShown -= 1;
            if (whichScreenShotIsShown < 0)
                whichScreenShotIsShown = files.Length - 1;
            GetPictureAndShowIt();
        }
    }

    public void ClickShare()
    {
        StartCoroutine(TakeScreenShot());
    }

    private IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        string filePath;

        ////스크린 캡쳐
        //Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //ss.Apply();

        ////스크린 캡쳐 저장 및 PNG로 변환
        //string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        //File.WriteAllBytes(filePath, ss.EncodeToPNG());

        //// To avoid memory leaks
        //Destroy(ss);

        ////공유 기능
        if (files.Length > 0)
        {
            filePath = files[whichScreenShotIsShown];
            new NativeShare().AddFile(filePath).SetSubject("Share test").SetText("Hello world!").Share();
        }
    }
}
