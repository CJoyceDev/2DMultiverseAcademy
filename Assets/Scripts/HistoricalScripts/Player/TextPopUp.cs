using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
   /* public TMP_Text SignText;
    [SerializeField] public string Textbox;*/
    /*public GameObject CanvasObject;
    public Canvas Canvas;
    RectTransform rectTransform;
    public bool Talking;*/

    // Start is called before the first frame update
    /*void Awake()
    {
        Canvas = CanvasObject.GetComponent<Canvas>();

    }*/

  /*  private void Awake()
    {
        SignText = SignText;
    }*/


    void MakeText()
    {

        /*SignText = Instantiate(signBox);
        SignText.transform.parent = CanvasObject.transform;
        SignText.text = Textbox;*/

        /*rectTransform = SignText.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(400, 250);*/

        Canvas.SetActive(true);
    }


    void BreakText()
    {
        StartCoroutine(SmoothClose());
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
          //Textbox = "hello world";
            /*Debug.Log("texthit");*/
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

    IEnumerator SmoothClose()
    {

        yield return new WaitForSeconds(1.0f);

        Canvas.SetActive(false);

        /*Destroy(SignText);
        
        if (Talking)
        {
            Destroy(this);
        }*/


    }

}
