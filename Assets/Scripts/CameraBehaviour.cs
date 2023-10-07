using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public void onCameraButtonPress()
    {
        System.DateTime dt = System.DateTime.Now;
        Debug.Log("Xenolingo_" + dt.ToString("yyyy-MM-ddTHH:mm:ssZ") + ".png saved!");
        ScreenCapture.CaptureScreenshot("Xenolingo_"+dt.ToString("yyyy-MM-ddTHH:mm:ssZ") +".png");
    }
}
