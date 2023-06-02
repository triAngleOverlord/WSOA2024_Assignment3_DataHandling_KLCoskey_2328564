using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static float hour;
    public static float minute;
    public static weekdays day;
    public static int dayCount;
    public static string dayName;
    [SerializeField] TextMeshProUGUI hourHand;
    [SerializeField] TextMeshProUGUI minuteHand;
    [SerializeField] TextMeshProUGUI dayCalender;

    public List<GameObject> farmers;
    public bool nightTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        hour = 0;
        minute=0;
        dayCount = 5;
        day = (weekdays)dayCount;

        foreach (Transform worker in GameObject.Find("FarmHouse").transform)
        {
            if (worker.CompareTag("Farmer"))
            {
                farmers.Add(worker.gameObject);
                worker.gameObject.SetActive(false);
            }
        }

        hourHand.text = new string("0" + hour.ToString() + ":");

        changeWeekday(day);
        dayCalender.text = new string(dayName);
        workerSchedule();
        InvokeRepeating("changeTheTime", 0.01f, 0.01f);

        

    }

    void changeTheTime()
    {
        minute++;

        if (minute == 60)
        {
            minute = 0;
            hour++;
            if (hour == 24)
            {
                hour = 0;
                dayCount += 1;
                if(dayCount==7)
                {
                    dayCount = 0;
                }
                day = (weekdays)dayCount;
                changeWeekday(day);
                dayCalender.text = new string(dayName);
                workerSchedule();
            }


            if (hour < 10)
            {
                hourHand.text = new string("0" + hour.ToString()+ ":");
            }
            else 
            {
                hourHand.text = new string(hour.ToString()+ ":");
                
            }
        }

        if (minute <10)
        {
            minuteHand.text = new string("0" + minute.ToString());
        }
        else
        {
            minuteHand.text = new string( minute.ToString());
        }

        

    }


    public void changeWeekday(weekdays weekday)
    {
        day = weekday;

        switch (day) 
        { 
            case weekdays.Mon: dayName = "Mon";
                break;
            case weekdays.Tues:
                dayName = "Tues";
                break;
            case weekdays.Wed:
                dayName = "Wed";
                break;
            case weekdays.Thurs:
                dayName = "Thurs";
                break;
            case weekdays.Fri:
                dayName = "Fri";
                break;
            case weekdays.Sat:
                dayName = "Sat";
                break;
            case weekdays.Sun:
                dayName = "Sun";
                break;

            default: throw new ArgumentOutOfRangeException(nameof(weekday), weekday, null);

        }
    }
    public enum weekdays
    {
        Mon, Tues, Wed, Thurs, Fri, Sat, Sun
    }

    public IEnumerator holdFarmer(GameObject holdFarmer)
    {
        holdFarmer.transform.GetComponent<MovingFarmer>().currentAnimal = null;
        holdFarmer.transform.GetComponent<MovingFarmer>().enabled = false;
        holdFarmer.transform.GetComponent<CanvasGroup>().alpha = 0.5f;
        holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmersInPen.Remove(holdFarmer.name);
        if(holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmersInPen.Count==0)
        {
            holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmerPresent = false;
        };
        yield return new WaitForSeconds(6);


        holdFarmer.transform.GetComponent<MovingFarmer>().enabled = true;
        holdFarmer.transform.GetComponent<CanvasGroup>().alpha = 1f;
        holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmersInPen.Add(holdFarmer.name);
        GameObject.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().ClearOptions();
        GameObject.Find("Dropdown").transform.GetComponent<TMP_Dropdown>().AddOptions(holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmersInPen);
        holdFarmer.transform.parent.transform.GetComponent<PenStats>().farmerPresent = true;
    }

    public void workerSchedule()
    {
        GameObject.Find("ActionsPopUp").transform.GetComponent<Actions>().cancel();
        if(day== weekdays.Mon || day==weekdays.Thurs|| day==weekdays.Fri)
        {
            farmers[0].SetActive(true);
            farmers[1].SetActive(false);farmers[1].transform.SetParent(GameObject.Find("FarmHouse").transform);
            farmers[2].SetActive(true);
            farmers[3].SetActive(false);  farmers[3].transform.SetParent(GameObject.Find("FarmHouse").transform);
        }

        else if(day==weekdays.Tues|| day==weekdays.Wed)
        {
            farmers[0].SetActive(false); farmers[0].transform.SetParent(GameObject.Find("FarmHouse").transform);
            farmers[1].SetActive(true);
            farmers[2].SetActive(false); farmers[2].transform.SetParent(GameObject.Find("FarmHouse").transform);
            farmers[3].SetActive(false); farmers[3].transform.SetParent(GameObject.Find("FarmHouse").transform);
        }
        else if(day==weekdays.Sat|| day==weekdays.Sun)
        {
            farmers[0].SetActive(true);
            farmers[1].SetActive(false); farmers[1].transform.SetParent(GameObject.Find("FarmHouse").transform);
            farmers[2].SetActive(false); farmers[2].transform.SetParent(GameObject.Find("FarmHouse").transform);
            farmers[3].SetActive(true);
        }
    }

    public void nightShift()
    {
        GameObject.Find("Clock").GetComponent<Image>().color = Color.blue;
    }
}
