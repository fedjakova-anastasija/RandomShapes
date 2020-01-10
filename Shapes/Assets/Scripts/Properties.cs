using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public int minShapeCount;
    public int maxShapeCount;

    public AudioSource winSound;
    public AudioSource loseSound;

    public Color winColor;
    public Color loseColor;

    public List<string> winText;
    public List<string> loseText;

    public int maxDifficultyLevel;
    public int minTimeValue;
    public int maxTimeValue;

    public float minRotationValue;
    // Уменьшение времени таймера в зависимости от уровня. Не ниже minTimeValue
    public int timeCup;
    // Прирост кол-ва фигур в зависимости от уровня. Не выше maxShapeCount
    public int shapesCup;
    // Прирост скорости вращения фигур в зависимости от уровня
    public float rotationSpeedCup;
    public int newShapeCup;
    private int defaultShapeTypeByLevelCount = 2;

    public int GetMaxTimerValue(int currentLevel)
    {
        int currentTimeLoseByLevel = timeCup * (currentLevel - 1);
        int currentTime = maxTimeValue - currentTimeLoseByLevel;
        if (currentTime <= minTimeValue) return minTimeValue;
        return currentTime;
    }

    public int GetCurrentLevelShapesCount(int currentLevel)
    {
        int shapesAdd = currentLevel * shapesCup;
        int currentShapeCount = minShapeCount + shapesAdd;
        if(currentShapeCount >= maxShapeCount) return maxShapeCount;
        return currentShapeCount;
    }

    public float GetCurrentLevelShapeRotationSpeed(int currentLevel)
    {
        return minRotationValue + (currentLevel - 1) * rotationSpeedCup;
    }

    public int GetCurrentLevelShapeTypesCount(int currentLevel)
    {
        if(currentLevel % newShapeCup == 0)
        {
            defaultShapeTypeByLevelCount++;
        }
        if(defaultShapeTypeByLevelCount >= 3)
        {
            defaultShapeTypeByLevelCount = 3;
        }
        return defaultShapeTypeByLevelCount;
    }
}
