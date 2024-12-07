using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutComponent : MonoBehaviour
{
    // editor Component가 좀 깔끔해 보이고 싶을떄에 [Header(" ")] 사용
    // [Tooltip(" ")] 은 변수에 대한 간단한 설명이나 정보를 제공하기 위해 사용
    // [Range(min, max)] 은 변수의 값을 슬라이더로 조정하는 역활을 수행
    [Header("Life"), Tooltip("생명은 중요하지")]
    public string data1;
    [Tooltip("생명은 중요하지2")]
    public string data2;
    
    [Header("Sleep"), Tooltip("잠은 중요하지")]
    public string data3;
    public string data32;
    [Tooltip("잠은 중요하지 2")]
    public string data33;
    
    [Header("Die"), Range(0.1f, 5f)]
    public float data4;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
