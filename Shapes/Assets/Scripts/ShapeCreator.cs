using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCreator : MonoBehaviour
{
    private List<GameObject> firstTypeShapes = new List<GameObject>();
    private List<GameObject> secondTypeShapes = new List<GameObject>();

    public Transform generatingCenter;

    public GameObject CreateShape(GameObject shapePrefab)
    {
        GameObject shape = Instantiate(shapePrefab);
        InitBasicShapeStats(shape);
        return shape;
    }

    private void InitBasicShapeStats(GameObject shape)
    {
        shape.transform.rotation = RandomRotation();
        shape.transform.SetParent(generatingCenter);
        shape.transform.localPosition = generatingCenter.position;
        shape.GetComponent<Renderer>().material.color = Random.ColorHSV();
        shape.AddComponent<RotationAround>();
    }

    private Quaternion RandomRotation()
    {
        return new Quaternion(Random.Range(30, 90), Random.Range(30, 90), Random.Range(30, 90), Random.Range(30, 90));
    }
}
