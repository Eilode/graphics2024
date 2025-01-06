/*****
 * ������ſ��Դ��ݵķ���
 * ֻҪ����Ҫ���ݵķ�����ֻ��Ҫ�����̳и÷��������ҵ�
 * ***/
using DataProtocol;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BaseSendOrder : MonoBehaviour
{
    protected virtual void Awake()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitUntil(() => OrderManager.Instance != null);
        OrderManager.Instance.BaseSendOrderList.Add(this);

    }

    protected virtual void OnDestroy()
    {
        OrderManager.Instance.BaseSendOrderList.Remove(this);
    }
}
