using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButtonScript : MonoBehaviour
{
    public void StartReplay()
    {
        Camera.main.GetComponent<Animator>().Play("battleRoyalReplay");
        _timeScaleStore = Time.timeScale;
        Time.timeScale = 0;
       _BuffetTable = GameObject.Find("Buffet Table");
        _LableCanvas = GameObject.Find("Lable Canvas");
        _BuffetTable.SetActive(false);
        _LableCanvas.SetActive(false);
        StartCoroutine("wait");
    }
    private float _timeScaleStore;
    GameObject _BuffetTable;
    GameObject _LableCanvas;
    private IEnumerator wait()
    {
        
            yield return new WaitForSecondsRealtime(5);
        _BuffetTable.SetActive(true);
        _LableCanvas.SetActive(true);
        Time.timeScale = _timeScaleStore;
        gameObject.SetActive(false);
    }
}
