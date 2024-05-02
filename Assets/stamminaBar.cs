using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stamminaBar : MonoBehaviour
{
    private StamminaControler _stamminaControler;
    private Image StamminaBar;
    // Start is called before the first frame update
    void Start()
    {
        _stamminaControler = FindObjectOfType<StamminaControler>();
        StamminaBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        StamminaBar.fillAmount = _stamminaControler.playerStammina / _stamminaControler.maxStammina;
    }
}

