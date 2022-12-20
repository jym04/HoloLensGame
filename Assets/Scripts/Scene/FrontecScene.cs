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

        Debug.Log($" ��������  : {temp}");

        /*
           ������� device_status
           �������귮 device_status
           ���ϻ��귮 #1 daily_count_1_A
           ���ϻ��귮 #2 daily_count_1_B
           ���� ���� �з� main_air_pressure
           ��� ��Ż �µ� ������ sholder_metal_control_temperature
           ��� ��Ż �µ� ������ sholder_metal_cutting_temperature
           ��Ȱ�� ���� �з�  lubricant_supply_pressure
           ������ ���� �з�  process_oil_pressure
           �����Ż ���ϰ��� �з� ������ sholder_metal_oil_supply_pressure_control
           �����Ż ���ϰ��� �з� ������ sholder_metal_oil_supply_cutting_control
           ��������Ʈ ���� ��� oil_pressure_level_warning
           ���θ��� ȸ���� (���귮 �ݿ�) main_motor_rpm

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
