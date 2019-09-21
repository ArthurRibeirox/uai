using UnityEditor;
using UnityEngine;

namespace Uai
{
    public static class Screenshot
    {
        [MenuItem("Tools/Take screenshot")]
        private static void TakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot("Screenshot.png");
        }
    }
}