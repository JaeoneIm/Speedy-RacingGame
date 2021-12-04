using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownValue : MonoBehaviour
{
    public Dropdown DbNumPeople;
    public static int getNum;
    public string[] arrayClass = new string[3] { "2명", "3명", "4명" };

    void Awake()
    {
        
    }
    void Start()
    {
        DbNumPeopleChange();
    }

    void Update()
    {
        
    }

    public void DbNumPeopleChange()
    {
        DbNumPeople.ClearOptions();

        List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();

        foreach (string str in arrayClass)
        {
            optionList.Add(new Dropdown.OptionData(str));
        }

        DbNumPeople.AddOptions(optionList);
        DbNumPeople.value = 2;
        getNum = DbNumPeople.value;

        DbNumPeople.onValueChanged.AddListener(OnDropdownEvent);
    }

    public void OnDropdownEvent(int index)
    {
        getNum = index;
    }
}
