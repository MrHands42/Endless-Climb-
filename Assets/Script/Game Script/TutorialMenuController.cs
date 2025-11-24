using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Button and GameObject
using TMPro; // Required if using TextMeshPro

public class TutorialMenuController : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the tutorial menu panel
    public Button tutorialButton;    // Reference to the tutorial button

    void Start()
    {
        // Ensure the panel is hidden at the start
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        // Assign the OnClick event for the button
        if (tutorialButton != null)
        {
            tutorialButton.onClick.AddListener(ToggleTutorialMenu);
        }
    }

    // This method will be called when the tutorial button is clicked
    public void ToggleTutorialMenu()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf); // Toggle visibility
        }
    }
}