using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAround : MonoBehaviour
{
    private Properties props;
    private GameController gameController;
    private float rotationSpeed;
    private Vector3 rotationAxis;

    private void Start()
    {
        props = FindObjectOfType<Properties>();
        gameController = FindObjectOfType<GameController>();
        int currentLevel = gameController.GetDifficultyLevel();
        rotationSpeed = props.GetCurrentLevelShapeRotationSpeed(currentLevel);
        rotationAxis = RandomRotationAxis();
    }

    private Vector3 RandomRotationAxis()
    {
        return new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
    }

    void Update()
    {
        gameObject.transform.Rotate(rotationAxis, rotationSpeed);
    }
}
