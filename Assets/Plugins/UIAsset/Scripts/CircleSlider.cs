using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleSlider : MonoBehaviour
{

	public bool b = true;
	public Image image;
	public float speed = 0.5f;
	public string unit = "°C";
	float time = 0f;
	public float range1, range2;
	private bool up = true;
	public float num;

	public Text progress;

	private void Start()
	{
		image.fillAmount = num;

	}

}

	
