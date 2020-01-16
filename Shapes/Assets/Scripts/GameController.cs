using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isGameStarted;

    private GameTimer timer;
    private UIController UI;
    private GameSceneCreator gameCreator;
    private Properties props;

    private int difficultyLevel = 1;
    public int GetDifficultyLevel() { return difficultyLevel; }

    enum LEVEL_STATE { WIN, LOSE }
    #region StartGameActions
    private void GetAllNecessaryObjects()
    {
        UI = FindObjectOfType<UIController>();
        timer = FindObjectOfType<GameTimer>();
        gameCreator = FindObjectOfType<GameSceneCreator>();
        props = FindObjectOfType<Properties>();
    }
    private void InitializeUI()
    {
        UI.InitializeUI();
    }

    private void ConfigureTimer()
    {
        timer.SetMaxTime(props.GetMaxTimerValue(difficultyLevel));
        timer.ResetTimer();
        timer.StartTick();
    }

    private void CreateShapes()
    {
        gameCreator.CreateShapes();
    }

    private void SetGameStarted(bool state)
    {
        isGameStarted = state;
    }
    #endregion
    public void StartGame()
    {
        SetGameStarted(false);
        GetAllNecessaryObjects();
        InitializeUI();
        ConfigureTimer();
        CreateShapes();
        SetGameStarted(true);
    }

    #region EndLevelActions
    private void ResetUI()
    {
        UI.ResetUI();
    }

    private void StopTimer()
    {
        timer.StopTick();
    }

    private void DestroyAllShapes()
    {
        gameCreator.DestroyShapes();
    }

    private void AddRigidbody(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
    }

    private void DropAllShapes()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        GameObject[] cylinders = GameObject.FindGameObjectsWithTag("Cylinder");
        foreach(var sphere in spheres)
        {
            AddRigidbody(sphere);
        }foreach(var cube in cubes)
        {
            AddRigidbody(cube);
        }foreach(var cylinder in cylinders)
        {
            AddRigidbody(cylinder);
        }
        
    }

    private void DestroyAll(GameObject[] objects)
    {
        foreach(var obj in objects)
        {
            Destroy(obj);
        }
    }

    private void DisableIncorrectShapes(string tag)
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        GameObject[] cylinders = GameObject.FindGameObjectsWithTag("Cylinder");

        if(tag == "Sphere")
        {
            DestroyAll(cubes);
            DestroyAll(cylinders);
        }
        if(tag == "Cube")
        {
            DestroyAll(spheres);
            DestroyAll(cylinders);
        }
        if (tag == "Cylinder")
        {
            DestroyAll(spheres);
            DestroyAll(cubes);
        }

    }

    private int uiLevel = 1;
    private void EndLevelActions(LEVEL_STATE state)
    {
        if(state == LEVEL_STATE.LOSE)
        {
            DropAllShapes();
        }
        if (state == LEVEL_STATE.WIN)
        {
            difficultyLevel++;
            uiLevel++;
            if (difficultyLevel > props.maxDifficultyLevel) difficultyLevel = props.maxDifficultyLevel;
        }

        ShowEndLevelText(state);
        PlayEndLevelSound(state);
        StartCoroutine(NextLevel());
    }

    private void PlayEndLevelSound(LEVEL_STATE state)
    {
        AudioSource endLevelSound = props.winSound;
        if(state != LEVEL_STATE.WIN)
        {
            endLevelSound = props.loseSound;
        }
        endLevelSound.Play();
    }

    private void ShowEndLevelText(LEVEL_STATE state)
    {
        string endLevelText = props.winText[Random.Range(0, props.loseText.Capacity)];
        Color endLevelColor = props.winColor;
        if (state != LEVEL_STATE.WIN)
        {
            endLevelText = props.loseText[Random.Range(0, props.loseText.Capacity)];
            endLevelColor = props.loseColor;
        }
        UI.ShowEndLevelText(endLevelText, endLevelColor);
    }
    #endregion
    private void EndLevel(LEVEL_STATE state)
    {
        SetGameStarted(false);
        ResetUI();
        StopTimer();
        EndLevelActions(state);        
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5);
        DestroyAllShapes();
        InitializeUI();
        ConfigureTimer();
        CreateShapes();
        SetGameStarted(true);
    }

    public void MakeChoose(string shapeTag)
    {
        List<string> shapeTags = new List<string>() { "Cube", "Sphere", "Pyramid", "Cylinder"};
        Dictionary<string, int> shapesCountByTag = new Dictionary<string, int>();

        foreach(string tag in shapeTags)
        {
            shapesCountByTag[tag] = GameObject.FindGameObjectsWithTag(tag).Length;
        }

        var maxValue = shapesCountByTag.Values.Max();
        string keyOfMaxValue = shapesCountByTag.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        if(keyOfMaxValue == shapeTag)
        {
            EndLevel(LEVEL_STATE.WIN);
            DisableIncorrectShapes(shapeTag);
        }
        else
        {
            EndLevel(LEVEL_STATE.LOSE);
        }
  
    }

    private void Update()
    {
        UI.UpdateCurrentLevelText(uiLevel);
        if (!isGameStarted) return;
        UI.UpdateTimer(timer.GetCurrentTime());

        if (timer.GetCurrentTime() == 0)
        {
            EndLevel(LEVEL_STATE.LOSE);
        }
    }
}
