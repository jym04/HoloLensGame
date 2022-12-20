using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Define;

public class Utils
{


	public static IEnumerator TextureLoad(string _url, string goName)
	{
		string url = _url;
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		yield return www.SendWebRequest();
		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(www.result);
		}
		else
		{
			//matching
			RawImage rawimg = GameObject.Find
				 (goName).GetComponent<RawImage>();
			rawimg.texture = (www.downloadHandler as DownloadHandlerTexture).texture;
		}
	}


	public static T ParseEnum<T>(string value, bool ignoreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if (transform != null)
                return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

	const int MAX_HP_ID = 6001;
	const int WORKABILITY_ID = 6011;
	const int LIKEABILITY_ID = 6021;
	const int LUCK_ID = 6041;
	const int STRESS_ID = 6031;
	const int BLOCK_ID = 6061;
	const int MONEY_ID = 6051;
	const int SALARY_ID = 6071;

	//직급
	const int Intern = 5000;
	const int Sinib = 5010;
	const int Daeri = 5020;
	const int Gwajang = 5030;
	const int Bujang = 5040;
	const int Esa = 5050;
	const int Sajang = 5060;




    public static string GetRewardValueString(int value)
	{
		string valueText = "";

		if (value > 0)
			valueText = $"+{value}";
		else
			valueText = $"{value}";

		return valueText;
	}

    public static Color GetRewardColor(RewardType type, int value)
	{
		// 스트레스는 줄어드는게 좋은거다
		if (type == RewardType.Stress)
		{
			if (value > 0)
				return new Color(0.08971164f, 0.5462896f, 0.9056604f);
			else
				return new Color(1.0f, 0, 0);
		}
		else
		{
			if (value < 0)
				return new Color(0.08971164f, 0.5462896f, 0.9056604f);
			else
				return new Color(1.0f, 0, 0);
		}
	}


    public static JobTitleType GetRandomNpc()
	{
        // 인턴, 신입 제외한 나머지에서 추출
        int randomCount = UnityEngine.Random.Range(2, JOB_TITLE_TYPE_COUNT);
        return (JobTitleType)randomCount;
	}


	public static string GetMoneyString(int value)
	{
		int money = value / 10000;
		return $"{money}만";
		//return string.Format("{0:0.0}만", value / 10000.0f);
	}
}
