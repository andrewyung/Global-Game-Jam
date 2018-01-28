using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text mainTextField;

    private static UIManager ui;

    void Awake()
    {
        if (ui == null)
        {
            ui = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void setText(string text)
    {
        mainTextField.text = text;
        mainTextField.color = Color.black;

        StartCoroutine(textDisableTimer(1.5f));
    }

    IEnumerator textDisableTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Color currentColor = mainTextField.color;
        while (currentColor.a > 0)
        {
            currentColor.a -= Time.deltaTime;
            mainTextField.color = currentColor;

            yield return new WaitForEndOfFrame();
        }
    }


}
