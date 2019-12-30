using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickProcessor : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void ChooseFirstType()
    {
        gameController.MakeChoose("Sphere");
    }

    public void ChooseSecondType()
    {
        gameController.MakeChoose("Cube");
    }

    public void ChooseThirdType()
    {
        gameController.MakeChoose("Cylinder");
    }
}
