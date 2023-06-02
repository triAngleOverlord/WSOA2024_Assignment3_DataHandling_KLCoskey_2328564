using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenStats : MonoBehaviour , IDropHandler
{
    public bool farmerPresent;

    public float penTotalHealth;
    public float penMeanHealth;

    public List<GameObject> animalsInPen;
    public GameObject penBar;
    public List <string> farmersInPen;


    public void Awake()
    {
        foreach (Transform animal in gameObject.transform)
        {
            if (animal.GetComponent<Animal>() != null)
            {
                animalsInPen.Add(animal.gameObject);
                penTotalHealth += animal.transform.GetComponent<Animal>().stats.maxHealth;
            }
            
        }

        penBar = transform.Find("PenBar").gameObject;
        penBar.GetComponent<Slider>().maxValue = penTotalHealth/animalsInPen.Count;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.gameObject.CompareTag("Farmer"))
            {
                eventData.pointerDrag.transform.SetParent(transform, true);
                farmersInPen.Add(eventData.pointerDrag.gameObject.name);
                //GameObject.Find("Dropdown").transform.GetComponent<Dropdown>().options.Add((Dropdown.OptionData)eventData.pointerDrag.transform.name);
                
                
            }
        }

    }

    public void Update()
    {
        penTotalHealth = new float();
        for (int i = 0; i < animalsInPen.Count; i++)
        {
            
            penTotalHealth += animalsInPen[i].transform.GetComponent<Animal>().stats._health;
        }

        penMeanHealth = penTotalHealth/ animalsInPen.Count;
        //Debug.Log(transform.name+"'s mean health is " + penMeanHealth);
        penBar.transform.GetComponent<Slider>().value = penMeanHealth;

        if(farmersInPen.Count > 0)
        {
            farmerPresent = true;
        }
        else if (farmersInPen.Count == 0)
        {
            farmerPresent = false;
        }

        
    }
}
