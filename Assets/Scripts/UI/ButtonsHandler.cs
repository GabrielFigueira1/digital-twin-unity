using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    [SerializeField]
    private InputField newName;
    [SerializeField]
    private InputField newValue;
    [SerializeField]
    private Inst inst;

    public Request req;

    public void DeleteLast()
    {
        req.DeleteLast((requestBody) =>
        {
            if (!IsRequestOK(requestBody)) return;

            ReadLast();
        }
        );
    }

    public void Send()
    {
        req.InsertData(newName.text, int.Parse(newValue.text), (requestBody) =>
        {
            if (!IsRequestOK(requestBody)) return;

            ReadLast();
        }
        );
    }

    public void ReadLast()
    {
        req.ReadLast((requestBody) =>
        {
            if (!IsRequestOK(requestBody)) return;
            inst.textFieldData = JsonUtility.FromJson<TextFieldData>(requestBody);
            Debug.Log(inst.textFieldData.name);
        }
        );
    }

    private bool IsRequestOK(string requestBody)
    {
        if (requestBody == "ERROR")
        {
            Debug.LogError("Error on request.");
            return false;
        }
        Debug.Log(requestBody);
        return true;
    }

    void Start()
    {
        ReadLast();
    }

}

