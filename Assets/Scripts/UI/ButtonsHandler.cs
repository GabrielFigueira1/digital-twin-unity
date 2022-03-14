using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    public ScrollViewHandler scrollView;

    public Request req;

    private Move product;

    public void StartSimulation()
    {
        req.StartSimulation((requestBody) =>
        {
            scrollView.Log(requestBody);
        }
        );
    }

    public void StopSimulation()
    {
        req.StopSimulation((requestBody) =>
        {
            product = GameObject.FindObjectOfType<Move>();
            if (product != null)
                Destroy(product.gameObject);
            scrollView.Log(requestBody);
        }
        );
    }

    public void LearnPlant()
    {
        req.LearnPlant((requestBody) =>
        {
            scrollView.Log(requestBody);
        }
        );
    }
}

