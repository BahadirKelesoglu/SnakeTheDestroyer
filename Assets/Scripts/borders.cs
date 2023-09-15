using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borders : MonoBehaviour
{
    [SerializeField] private float dlg = 40f;
    [SerializeField] private float ElemanBoyutu = 40f;
    [SerializeField] private float istenilenAralik = 100f;

    [SerializeField] private RectTransform rt;
    private int elemanMik;
    private float IcerikBoyutu;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        elemanMik = rt.childCount;
        IcerikBoyutu = ((elemanMik * (ElemanBoyutu)) - istenilenAralik) * rt.localScale.y;

        if (rt.localPosition.y > IcerikBoyutu)
            rt.localPosition = new Vector3(rt.localPosition.x, IcerikBoyutu, rt.localPosition.z);
        else if (rt.localPosition.y < -dlg)
            rt.localPosition = new Vector3(rt.localPosition.x, -dlg, rt.localPosition.z);
    }

}
