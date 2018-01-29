using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {

    [SerializeField]
    private Vector3 initialPosition = new Vector3(0, 13, 11);
    [SerializeField]
    private Vector3 offsetIncrements = new Vector3(0, -3, 0);
    [SerializeField]
    private Vector3 initialScale = new Vector3(22, 0.2f, 22);
    [SerializeField]
    private Vector3 downsizeIncrements = new Vector3(-3, 0, -3);

    [SerializeField]
    private GameObject[] layerGameObjects;

    private static int currentLayerIndex;
    private static bool layerAnimating;

    public static bool isLayerAnimating()
    {
        return layerAnimating;
    }

    private static LayerManager lm;

    void Awake()
    {
        lm = this;
    }

    // Use this for initialization
    void Start() {
        layerGameObjects[0].transform.position = initialPosition;
        layerGameObjects[0].transform.localScale = initialScale;
        for (int i = 1; i < layerGameObjects.Length; i++)
        {
            layerGameObjects[i].transform.position = initialPosition + (offsetIncrements * i);
            layerGameObjects[i].transform.localScale = initialScale + (downsizeIncrements * i);
        }
        reset();
    }

    private void incrementCurrentLayerIndex()
    {
        if (currentLayerIndex + 1 == layerGameObjects.Length)
        {
            currentLayerIndex = 0;
        }
        else
        {
            currentLayerIndex++;
        }
    }
    private void decrementCurrentLayerIndex()
    {
        if (currentLayerIndex - 1 == -1)
        {
            currentLayerIndex = layerGameObjects.Length - 1;
        }
        else
        {
            currentLayerIndex--;
        }
    }

    public static void reset()
    {
        currentLayerIndex = 0;
        layerAnimating = false;
    }

    public static GameObject getCurrentLayerGameObject()
    {
        GameObject currentLayerGameObject = lm.layerGameObjects[currentLayerIndex];

        return currentLayerGameObject;
    }

    public static GameObject getNextLayerGameObject()
    {
        lm.incrementCurrentLayerIndex();
        GameObject nextLayerGameObject = lm.layerGameObjects[currentLayerIndex];
        lm.decrementCurrentLayerIndex();

        return nextLayerGameObject;
    }

    public static void nextLayer(bool validChange)
    {
        GameManager.layerChange(currentLayerIndex, validChange);
        if (validChange)
        {
            lm.StartCoroutine(lm.animateLayer());
        }
    }

    private IEnumerator animateLayer()
    {
        if (layerAnimating)
        {
            yield break;
        }
        layerAnimating = true;

        Vector3 remainingOffset = offsetIncrements;
        float totalDeltaTime = 0;
        while (true)
        {
            float deltaTime = Time.deltaTime;
            totalDeltaTime += deltaTime;
            if (totalDeltaTime > 1)
            {
                deltaTime -= totalDeltaTime - 1;
            }

            layerGameObjects[currentLayerIndex].transform.position -= offsetIncrements * deltaTime * 50;
            layerGameObjects[currentLayerIndex].transform.localScale -= downsizeIncrements * deltaTime * 100;
            for (int i = 0; i < layerGameObjects.Length; i++)
            {
                if (i != currentLayerIndex)
                {
                    layerGameObjects[i].transform.position -= offsetIncrements * deltaTime;
                    layerGameObjects[i].transform.localScale -= downsizeIncrements * deltaTime;
                }
            }

            if (totalDeltaTime > 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return resetCurrentLayer();

        layerAnimating = false;
    }

    private IEnumerator resetCurrentLayer()
    {
        layerGameObjects[currentLayerIndex].transform.position = initialPosition + (offsetIncrements * (layerGameObjects.Length + 2));
        layerGameObjects[currentLayerIndex].transform.localScale = initialScale + (downsizeIncrements * (layerGameObjects.Length + 2));
        float totalDeltaTime = 0;
        while (true)
        {
            float deltaTime = Time.deltaTime * 3;
            totalDeltaTime += deltaTime;
            if (totalDeltaTime > 1)
            {
                deltaTime -= totalDeltaTime - 1;
            }
            layerGameObjects[currentLayerIndex].transform.position -= offsetIncrements * deltaTime * 3;
            layerGameObjects[currentLayerIndex].transform.localScale -= downsizeIncrements * deltaTime * 3;

            if (totalDeltaTime > 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        if (currentLayerIndex + 1 == layerGameObjects.Length)
        {
            currentLayerIndex = 0;
        }
        else
        {
            currentLayerIndex++;
        }
    }
}
