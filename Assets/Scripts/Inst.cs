using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inst : MonoBehaviour
{
    public TextFieldData textFieldData;

    public void Awake()
    {
        textFieldData = new TextFieldData();
    }
}