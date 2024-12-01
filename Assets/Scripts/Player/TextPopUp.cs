using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPopUp : MonoBehaviour
{

    [SerializeField] public TMP_Text signBox;
    public TMP_Text SignText;
    [SerializeField] public string Textbox;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void MakeText()
    {

        SignText = Instantiate(signBox);
        SignText.text = Textbox;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Textbox = "hello world";
            Debug.Log("texthit");
            MakeText();
          //Destroy(object);

        }

    }
}
