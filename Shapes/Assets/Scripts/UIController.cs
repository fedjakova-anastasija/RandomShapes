using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject timerObject;
    public GameObject circleButton;
    public GameObject cubeButton;
    public GameObject cylinderButton;
    public GameObject endLevelText;
    public GameObject currentLevelText;
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
        if(props.GetCurrentLevelShapeTypesCount(gameController.GetDifficultyLevel()) == 3)
        {
            cylinderButton.SetActive(true);
        }
    }

    public void ResetUI()
    {
        timerObject.SetActive(false);
        circleButton.SetActive(false);
        cubeButton.SetActive(false);
        cylinderButton.SetActive(false);
    }

    public void ShowEndLevelText(string text, Color color)
    {
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
        timerObject.GetComponent<Text>().text = timerValue.ToString();
    }

    public void UpdateCurrentLevelText(int currentLevel)
    {
        currentLevelText.GetComponent<Text>().text = string.Format("Level {0}", currentLevel.ToString());
    }
}