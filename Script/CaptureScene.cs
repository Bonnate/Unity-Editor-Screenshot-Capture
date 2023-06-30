/*
 * CaptureScene - Unity Editor Window for capturing screenshots
 * Author: [Bonnate] https://bonnate.tistory.com/
 * Repository: https://github.com/Bonnate/Unity-Editor-Screenshot-Capture
 * License: None (Provided without any explicit license)
 *
 * Description:
 * This script defines a custom Unity Editor Window for capturing screenshots in the Unity Editor. 
 * It allows the user to select the desired resolution, specify custom resolutions, and capture the screenshot.
 * The captured screenshot is saved to a specified folder in the project and can be automatically opened.
 *
 * Instructions:
 * 1. Attach this script to an Editor folder in your Unity project.
 * 2. To open the CaptureScene window, go to Tools -> Bonnate -> Capture using Scene Camera.
 * 3. Adjust the resolution settings and choose whether to use the scene camera ratio.
 * 4. Click "Capture Screenshot" to capture the screenshot and save it to the specified folder.
 *
 * Note: This script is provided as-is and may be subject to certain limitations and issues. 
 * Please refer to the repository for the latest updates and to report any issues.
*/

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;

public class CaptureScene : EditorWindow
{
    // Define optional resolution
    private enum Resolution
    {
        _HD = 1280,
        _FHD = 1920,
        _QHD = 2560,
        _4K = 3840,
        _8K = 7680,

        // Specify the resolution directly without using the specified resolution.
        Custom
    }

    private Resolution mSelectedResolution = Resolution._FHD;

    private string mAspectRationWidth;

    private string mAspectRatioHeight;

    private bool mIsUseSceneRatio;

    [MenuItem("Tools/Bonnate/Capture using Scene Camera")]

    private static void Init()
    {
        CaptureScene window = (CaptureScene)EditorWindow.GetWindow(typeof(CaptureScene));

        window.Show();
    }

    private void OnGUI()
    {
        // Display a dropdown list for selecting a resolution
        mSelectedResolution = (Resolution)EditorGUILayout.EnumPopup("Resolution", mSelectedResolution);

        if (mSelectedResolution == Resolution.Custom)
        {
            // Call the method to parse a custom resolution
            ParseCustomResolution();
        }
        else
        {
            // Call the method to show aspect ratio fields
            ShowAspectRatioFields();
        }

        // Display a toggle for enabling scene ratio
        mIsUseSceneRatio = EditorGUILayout.Toggle("Use Scene Ratio", mIsUseSceneRatio);

        // Add some space
        GUILayout.Space(10);

        // Display a button for capturing a screenshot
        if (GUILayout.Button("Capture Screenshot"))
        {
            // Call the method to capture a screenshot
            CaptureScreenshot();
        }

        // Add more space
        GUILayout.Space(20);

        // Begin a horizontal layout group
        GUILayout.BeginHorizontal();

        // Display a label with the text "Powered by: Bonnate" at the bottom of the window
        EditorGUILayout.LabelField("Powered by: Bonnate");

        // Create a button with the label "Github" that opens a URL when clicked
        if (GUILayout.Button("Github", GetHyperlinkLabelStyle()))
        {
            // Call the method to open the GitHub URL
            OpenURL("https://github.com/bonnate");
        }

        // Create a button with the label "Blog" that opens a URL when clicked
        if (GUILayout.Button("Blog", GetHyperlinkLabelStyle()))
        {
            // Call the method to open the Blog URL
            OpenURL("https://bonnate.tistory.com/");
        }

        // End the horizontal layout group
        GUILayout.EndHorizontal();
    }

    // Returns a GUI style for a hyperlink-like label
    private GUIStyle GetHyperlinkLabelStyle()
    {
        // Create a new GUIStyle based on the default label style
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = new Color(0f, 0.5f, 1f); // Blue color
        style.stretchWidth = false;
        style.wordWrap = false;
        return style;
    }

    // Opens the provided URL with the default application
    private void OpenURL(string url)
    {
        EditorUtility.OpenWithDefaultApp(url);
    }

    // Parses and handles the custom resolution input
    private void ParseCustomResolution()
    {
        if (mSelectedResolution != Resolution.Custom)
        {
            return;
        }

        if (int.TryParse(mAspectRationWidth, out int width) && int.TryParse(mAspectRatioHeight, out int height))
        {
            // Valid custom resolution input
        }
        else
        {
            // Invalid custom resolution input, fallback to default values
            width = 1920;
            height = 1080;
        }

        mAspectRationWidth = EditorGUILayout.IntField("Aspect Ratio Width", width).ToString();

        if (mIsUseSceneRatio)
        {
            EditorGUILayout.LabelField("Aspect Ratio Height", ((int)(width / GetSceneAspectRatio())).ToString());
            mAspectRatioHeight = ((int)(width / GetSceneAspectRatio())).ToString();
        }
        else
        {
            mAspectRatioHeight = EditorGUILayout.IntField("Aspect Ratio Height", height).ToString();
        }
    }

    // Shows the aspect ratio fields based on the selected resolution
    private void ShowAspectRatioFields()
    {
        mAspectRationWidth = ((int)mSelectedResolution).ToString();
        mAspectRatioHeight = ((int)((float)mSelectedResolution * 0.5625f)).ToString();

        EditorGUILayout.LabelField("Aspect Ratio Width", mAspectRationWidth);

        if (mIsUseSceneRatio)
        {
            EditorGUILayout.LabelField("Aspect Ratio Height", ((int)(float.Parse(mAspectRationWidth) / GetSceneAspectRatio())).ToString());
            mAspectRatioHeight = ((int)(float.Parse(mAspectRationWidth) / GetSceneAspectRatio())).ToString();
        }
        else
        {
            EditorGUILayout.LabelField("Aspect Ratio Height", mAspectRatioHeight);
        }
    }

    // Returns the aspect ratio of the scene's camera
    public float GetSceneAspectRatio()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        Camera camera = sceneView.camera;

        int width = camera.pixelWidth;
        int height = camera.pixelHeight;

        float aspectRatio = (float)width / height;
        return aspectRatio;
    }

    // Captures a screenshot with the specified resolution and saves it to a file
    private void CaptureScreenshot()
    {
        string timestamp = System.DateTime.Now.ToString("yyMMddHHmmssff");
        string folderPath = "Assets/Screenshots/";
        string fileName = "screenshot" + timestamp + ".png";
        string filePath = folderPath + fileName;

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        int width;
        int height;

        if (mSelectedResolution == Resolution.Custom && int.TryParse(mAspectRationWidth, out width) && int.TryParse(mAspectRatioHeight, out height))
        {
            // Custom resolution
        }
        else
        {
            width = (int)mSelectedResolution;

            if (mIsUseSceneRatio)
            {
                height = int.Parse(mAspectRatioHeight);
            }
            else
            {
                height = (int)((float)width * (int.Parse(mAspectRatioHeight) / (float)int.Parse(mAspectRationWidth)));
            }
        }

        SceneView sceneView = SceneView.lastActiveSceneView;
        Camera camera = sceneView.camera;

        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        camera.targetTexture = renderTexture;
        camera.Render();

        screenshotTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshotTexture.Apply();

        byte[] bytes = screenshotTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);

        RenderTexture.active = currentRT;
        camera.targetTexture = null;

        Debug.Log("Screenshot captured and saved to: " + filePath);

        AssetDatabase.Refresh();

        Object screenshotAsset = AssetDatabase.LoadAssetAtPath<Object>(filePath);
        Selection.activeObject = screenshotAsset;
    }

}

#endif
