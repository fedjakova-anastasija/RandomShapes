using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject timerObject;
    public GameObject timerObjectPanel;
    public GameObject circleButton;
    public GameObject cubeButton;
    public GameObject cylinderButton;
    public GameObject endLevelText;
    public GameObject endLevelTextPanel;
    public GameObject currentLevelText;
    public GameObject currentLevelTextPanel;
    private Properties props;
    private GameController gameController;

    private void Awake()
    {
        props = FindObjectOfType<Properties>();
        gameController = FindObjectOfType<GameController>();

    }
    public void InitializeUI()
    {
        timerObject.SetActive(true);
        circleButton.SetActive(true);
        cubeButton.SetActive(true);
        endLevelText.SetActive(false);
        endLevelTextPanel.SetActive(false);
        currentLevelTextPanel.SetActive(false);
        timerObjectPanel.SetActive(false);
        circleButton.GetComponent<Button>().enabled = true;
        cubeButton.GetComponent<Button>().enabled = true;
        if (props.GetCurrentLevelShapeTypesCount(gameController.GetDifficultyLevel()) == 3)
        {
            cylinderButton.SetActive(true);
            cylinderButton.GetComponent<Button>().enabled = true;
        }
    }

    public void ResetUI()
    {
        timerObject.SetActive(false);
        timerObjectPanel.SetActive(false);
        circleButton.GetComponent<Button>().enabled = false;
        cubeButton.GetComponent<Button>().enabled = false;
        cylinderButton.GetComponent<Button>().enabled = false;
    }

    public void ShowEndLevelText(string text, Color color)
    {
        endLevelTextPanel.SetActive(true);
        endLevelText.SetActive(true);
        endLevelText.GetComponent<Text>().text = text;
        endLevelText.GetComponent<Text>().color = color;
    }

    public void HideEndLevelText()
    {
        endLevelText.SetActive(false);
    }

    public void UpdateTimer(int timerValue)
    {
        timerObjectPanel.SetActive(true);
        timerObject.GetComponent<Text>().text = timerValue.ToString();
    }

    public void UpdateCurrentLevelText(int currentLevel)
    {
        currentLevelTextPanel.SetActive(true);
        currentLevelText.GetComponent<Text>().text = string.Format("Level {0}", currentLevel.ToString());
    }
}