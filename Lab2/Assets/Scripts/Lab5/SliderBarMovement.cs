using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBarMovement : MonoBehaviour {

    private RectTransform rectTransform;
    private float sinValue;

    [SerializeField]
    private float widthOfBar = 400;
    [SerializeField]
    private float speed = 3;

    public float hitPercentage
    {
        get { return 1 - Mathf.Abs(rectTransform.anchoredPosition.x / (sinValue)); }
    }

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        sinValue = (widthOfBar / 2) - (rectTransform.sizeDelta.x / 2);
	}
	
	// Update is called once per frame
	void Update () {
        rectTransform.anchoredPosition = new Vector2((sinValue) * Mathf.Sin((3 * Time.fixedTime) / (Mathf.PI)), 0);
	}
}
