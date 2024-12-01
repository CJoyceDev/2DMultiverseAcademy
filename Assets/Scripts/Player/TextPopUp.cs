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
    public GameObject CanvasObject;
    public Canvas Canvas;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = CanvasObject.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void MakeText()
    {

        SignText = Instantiate(signBox);
        SignText.transform.parent = CanvasObject.transform;
        SignText.text = Textbox;

        rectTransform = SignText.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);


    }


    void BreakText()
    {

        Destroy(SignText);


    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
          //Textbox = "hello world";
            Debug.Log("texthit");
            MakeText();
            //Destroy(gameObject);

        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            BreakText();

        }

    }
}
