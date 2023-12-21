using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _xImage;
    [SerializeField] private GameObject _oImage;
    
    public enum ViewType
    {
        X,
        None,
        O
    }

    public void SetClickEvent(Action callback)
    {
        _button.onClick.AddListener(callback.Invoke);
    }
    
    public void UpdateView(ViewType viewType)
    {
        _xImage.SetActive(viewType == ViewType.X);
        _oImage.SetActive(viewType == ViewType.O);
    }
}