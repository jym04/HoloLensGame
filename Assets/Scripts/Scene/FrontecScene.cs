using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noah;

public class FrontecScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init())
            return false;

        SceneType = Define.Scene.Frontec;


        return true;
    }

    private void Start()
    {

        //Test script 
        Dictionary<int, Noah.Record> testAddress = Managers.Data.AddressSpaceDatas;

        Noah.Record record = testAddress[9];
        foreach (var message in record.message)
        {
            Debug.Log(message.name);
        }
        var temp =  record.message.Find(x => x.name == "device_status").value;

        Debug.Log($" 상태정보  : {temp}");

        /*
           설비상태 device_status
           누적생산량 device_status
           일일생산량 #1 daily_count_1_A
           일일생산량 #2 daily_count_1_B
           메인 에어 압력 main_air_pressure
           어깨 메탈 온도 조작측 sholder_metal_control_temperature
           어깨 메탈 온도 절단측 sholder_metal_cutting_temperature
           윤활유 공급 압력  lubricant_supply_pressure
           가공유 공급 압력  process_oil_pressure
           어깨메탈 오일공급 압력 조작측 sholder_metal_oil_supply_pressure_control
           어깨메탈 오일공급 압력 절단측 sholder_metal_oil_supply_cutting_control
           유압유니트 레벨 경고 oil_pressure_level_warning
           메인모터 회전수 (생산량 반영) main_motor_rpm

         *  device_status
         *  cumulative_count_1
         *  daily_count_1_A
         *  daily_count_1_B
         *  main_air_pressure
         *  sholder_metal_control_temperature
         *  sholder_metal_cutting_temperature
         *  lubricant_supply_pressure
         *  process_oil_pressure
         */

    }


}
