# CaptureScene

`CaptureScene` is an editor window script for Unity that allows you to capture screenshots of the scene with customizable resolution options.

## Features

- Choose from predefined resolutions or set a custom resolution.
- Automatically calculates the aspect ratio based on the selected resolution.
- Saves the captured screenshot as a PNG file in the specified folder.
- Refreshes the Unity Editor's project folder to display the newly created screenshot.

## Usage

1. Open the Unity Editor.
2. In the Unity Editor menu, go to **Window > Bonnate > CaptureScene** to open the `CaptureScene` window.
3. In the window, select the desired resolution from the **Resolution** dropdown menu. If you choose **Custom**, enter the custom aspect ratio width and height in the respective text fields.
4. Click the **Capture Screenshot** button to capture a screenshot of the scene with the specified resolution.
5. The screenshot will be saved in the **Assets/Screenshots/** folder with a timestamped filename (e.g., screenshotYYMMddHHmmssff.png).
6. The Unity Editor's project folder will be refreshed to display the newly created screenshot.
7. When you enable the **[Use Scene Ratio] toggle**, you can take pictures at the **rate of the current Scene screen** while maintaining the width size.

Here is a video example demonstrating how to use the `CaptureScene` script: [YouTube link](https://www.youtube.com/watch?v=FjhwZGap78I)

## Installation

1. Download the **CaptureScene.cs** script from the GitHub repository
2. In your Unity project, navigate to the **Assets** folder.
3. *This script is already set up for Editor only, so it doesn't matter where you are in the folder.*
4. Copy the **CaptureScene.cs** script into the **Assets/Editor** folder.
5. The script will be automatically compiled by Unity and available for use.

## Requirements

- Unity Editor

## Notes

- Make sure to customize the folder path in the `CaptureScreenshot` method if you want to save the screenshots in a different location.
- This script is intended for use in the Unity Editor and may not work in a standalone build.

## License

This script does not have any license, and you can modify it, deploy it, or use it freely. enjoy!

## Credits

- Developed by [Bonnate](https://bonnate.tistory.com/)
