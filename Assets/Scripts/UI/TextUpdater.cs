using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField]
    private Text textName;
    [SerializeField]
    private Text textValue;
    [SerializeField]
    private Inst inst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textName.text = inst.textFieldData.name;
        textValue.text = inst.textFieldData.value.ToString();
    }
}
