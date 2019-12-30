using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCreator : MonoBehaviour
{
    private GameController gameController;
    private ShapeCreator shapeCreator;
    private Properties props;
    private List<GameObject> shapes = new List<GameObject>();

    public GameObject spherePrefab;
    public GameObject cubePrefab;
    public GameObject pyramidPrefab;
    public GameObject cylinderPrefab;

    public GameObject center;
    public Vector3 size;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        shapeCreator = FindObjectOfType<ShapeCreator>();
        props = FindObjectOfType<Properties>();
    }

    public void CreateShapes()
    {
        int currentLevel = gameController.GetDifficultyLevel();
        // Сколько всего фигур генеритсяя на текущем уровне
        int currentLevelShapeCount = props.GetCurrentLevelShapesCount(currentLevel);

        int currentShapeTypesByLevel = props.GetCurrentLevelShapeTypesCount(currentLevel);

        if(currentShapeTypesByLevel == 2)
        {
            int firstTypeCount = Random.Range(props.minShapeCount, currentLevelShapeCount - 1);
            int secondTypeCount = currentLevelShapeCount - firstTypeCount;
            for (int i = 0; i < firstTypeCount; i++)
            {
                GameObject shape = shapeCreator.CreateShape(spherePrefab);
                shape.transform.position = CreateRandomPosition();
                shapes.Add(shape);
            }
            for (int i = 0; i < secondTypeCount; i++)
            {
                GameObject shape = shapeCreator.CreateShape(cubePrefab);
                shape.transform.position = CreateRandomPosition();
                shapes.Add(shape);
            }
        }
        if (currentShapeTypesByLevel == 3) {
            int firstTypeCount = Random.Range(props.minShapeCount, currentLevelShapeCount - 3);
            int secondTypeCount = Random.Range(props.minShapeCount, currentLevelShapeCount - firstTypeCount);
            int thirdTypeCount = Random.Range(props.minShapeCount, currentLevelShapeCount - secondTypeCount);
            Debug.LogError(firstTypeCount);
            Debug.LogError(secondTypeCount);
            Debug.LogError(thirdTypeCount);
            for (int i = 0; i < firstTypeCount; i++)
            {
                GameObject shape = shapeCreator.CreateShape(spherePrefab);
                shape.transform.position = CreateRandomPosition();
                shapes.Add(shape);
            }
            for (int i = 0; i < secondTypeCount; i++)
            {
                GameObject shape = shapeCreator.CreateShape(cubePrefab);
                shape.transform.position = CreateRandomPosition();
                shapes.Add(shape);
            }
            for (int i = 0; i < thirdTypeCount; i++)
            {
                GameObject shape = shapeCreator.CreateShape(cylinderPrefab);
                shape.transform.position = CreateRandomPosition();
                shapes.Add(shape);
            }
        }
    }

    private Vector3 CreateRandomPosition()
    {
        Vector3 newPosition = center.transform.position + new Vector3(
            Random.Range(-size.x / 2, size.x / 2),
            Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2)
            );
        return newPosition;
    }

    public void DestroyShapes()
    {
        foreach(var shape in shapes)
        {
            Destroy(shape);
        }
        shapes.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center.transform.position, size);
    }

}
