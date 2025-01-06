using Netcore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UnityNetworkManager : MonoBehaviour
{
    public string Ip;
    public int Port;
    public static UnityNetworkManager Instance { get; private set;  }
    public Queue<byte[]> ReceiveDataQueue = new Queue<byte[]>();

    private void Awake()
    {
        Instance = this;
        //ע�����ӣ��Ͽ��������¼�
        SocketClient.Instance.ConnectResultEvent += OnConnectResult;
        SocketClient.Instance.DataReceiveEvent += OnDataReceive;
        SocketClient.Instance.DisconnectEvent += OnDisConnect;
    }

    private void OnDisConnect()
    {
        Debug.Log("OnDisconnect");
    }

    private void OnDataReceive(byte[] data)
    {
        ReceiveDataQueue.Enqueue(data);
    }

    private void OnConnectResult(bool result)
    {
        Debug.Log("connect result:" + result);
        if(result)
        {
            UIManager.Instance.OnConnect();
            //��ʼ����������
            StartCoroutine(SendHeart());
        }
    }

    /// <summary>
    /// �������������û����Ӻ��粻�����������������֪���û��Ƿ�������״̬����ͨ��ÿ5�뷢��һ���հ��ķ�ʽȷ������
    /// </summary>
    /// <returns></returns>
    IEnumerator SendHeart()
    {
        while(SocketClient.Instance.Connected)
        {
            yield return new WaitForSeconds(5);//����ʱ����
            //������������
            Send(new DataModel());//����һ���հ�
        }
    }

    public void Send(DataModel model)
    {
        if(!SocketClient.Instance.Connected)
        {
            Debug.Log("off line");
            return;
        }
        //byte[] message = model.Message;
        //int messageLength = message == null ? 0 : message.Length;
        //byte[] len = BitConverter.GetBytes(messageLength+2);
        ////��Ϣ�ܳ���
        //byte[] buffer = new byte[messageLength + 2+4]; //�������ֽڵ���Ϣ���ͺ���������   4�ֽڵ���Ϣ����
        ////������Ϣ��װ˳����Ϣ����+��Ϣ����+��Ϣ����˳���װ
        //Buffer.BlockCopy(len, 0, buffer, 0, 4);
        //byte[] code = new byte[2] { model.Type, model.Reequest };
        //Buffer.BlockCopy(code, 0, buffer, 4, 2);
        ////��Ϣ����Ϊ�ղŸ�ֵ
        //if(message!=null)
        //{
        //    Buffer.BlockCopy(message, 0, buffer, 6, messageLength);
        //}
        //������Ϣ
        SocketClient.Instance.Send(MessageCodeC.Encode(model));//ͨ��ʹ����д��ķ��������ظ��ű��ı�д
    }

    public void Connect()
    {
        IPAddress iPAddress;
        if (!IPAddress.TryParse(Ip, out iPAddress) || Port < 0 || Port > 65535)
        {
            Debug.LogError("ip or port is wrong");
            return;
        }
        SocketClient.Instance.ConnectServer(Ip, Port);

    }

    public void DisConnect()
    {
        SocketClient.Instance.DisConnectServer();
    }
}
