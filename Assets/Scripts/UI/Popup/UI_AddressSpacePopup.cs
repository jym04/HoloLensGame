using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Noah;

namespace Noah
{

    public class UI_AddressSpacePopup : UI_Popup
    {
    

        public TMP_Dropdown addressSpaceList;
        // Start is called before the first frame update
        void Start()
        {
            config();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void config()
        {
            Dictionary<int, Record> testAddress = Managers.Data.AddressSpaceDatas;

            Noah.Record record = testAddress[9];
            foreach (var message in record.message)
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = message.name;
                addressSpaceList.options.Add(option);
            }

        }
    }

}