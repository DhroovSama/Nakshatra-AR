using UnityEngine;
using UnityEngine.UI;

public class PNGSequencePlayer : MonoBehaviour
{
    public RawImage rawImage;      // The RawImage component where the sequence will be played
    public Texture2D[] frames;     // Array to hold the PNG frames
    public float frameRate = 30f;  // Frames per second

    private int currentFrame;
    private float timer;

    void Start()
    {
        if (frames.Length == 0)
        {
            Debug.LogError("No frames assigned to the PNG sequence.");
            return;
        }
        rawImage.texture = frames[0];  // Set the first frame initially
    }

    void Update()
    {
        if (frames.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;  // Loop through the frames
            rawImage.texture = frames[currentFrame];            // Update the RawImage texture
            timer -= 1f / frameRate;                            // Reset the timer
        }
    }
}
