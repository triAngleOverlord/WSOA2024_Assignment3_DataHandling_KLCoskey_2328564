using UnityEngine;
using UnityEngine.EventSystems;

public class MovingFarmer : MonoBehaviour ,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public RectTransform rectFarmer;
    public CanvasGroup canvasGPFarmer;
    public GameObject[] currentPen;
    public string currentAnimal;
    //public bool working;

    public void Awake()
    {
        rectFarmer = GetComponent<RectTransform>();
        canvasGPFarmer = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGPFarmer.blocksRaycasts = false;

        if (transform.parent.transform.CompareTag("Pen"))
        {
            
            currentPen = transform.parent.transform.GetComponent<PenStats>().animalsInPen.ToArray();
            transform.parent.transform.GetComponent<PenStats>().farmersInPen.Remove(gameObject.name);
            if (transform.parent.transform.GetComponent<PenStats>().farmersInPen.Count == 0)
            {
                transform.parent.transform.GetComponent<PenStats>().farmerPresent = false;

            }
                
            /*
            for (int i = 0; i < currentPen.Length; i++)
            {
                currentPen[i].transform.GetComponent<Button>().interactable = false;
            }*/
        }
        transform.SetParent(canvas.transform, true);
        currentAnimal = null;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectFarmer.anchoredPosition += eventData.delta / canvas.scaleFactor;
     
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Farmer has been dropped");
        canvasGPFarmer.blocksRaycasts = true;

        if (transform.parent.transform.CompareTag("Pen"))
        {
            transform.parent.transform.GetComponent<PenStats>().farmerPresent = true;
            GameObject.Find("ActionsPopUp").transform.GetComponent<Actions>().cancel();
            //currentPen = new GameObject[];
            /*currentPen = transform.parent.transform.GetComponent<PenStats>().animalsInPen.ToArray();
            for(int i = 0; i < currentPen.Length; i++)
            {
                currentPen[i].transform.GetComponent<Button>().interactable = true;
            }*/

        }
    }

    

    
}
