using DataProtocol;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class MessageSendManager : MonoBehaviour
{

    public static MessageSendManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �����û���
    /// </summary>
    /// <param name="username"></param>
    public void UpdateName(string username)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.UPDATA_NAME_C;
        model.Message = Encoding.UTF8.GetBytes(username);
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ��ȡ�����б�
    /// </summary>
    public void GetRoomList()
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.GET_ROOMLIST_C;
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="roomName"></param>
    public void CreateRoom(string roomName)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_CREATE_C;
        model.Message = Encoding.UTF8.GetBytes(roomName);
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ���뷿��
    /// </summary>
    /// <param name="roomName"></param>
    public void JoinRoom(string roomName)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_JOIN_C;
        model.Message = Encoding.UTF8.GetBytes(roomName);
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// �뿪����
    /// </summary>
    public void LeaveRoom()
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_LEAVE_C;
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// �ϴ�ê��
    /// </summary>
    /// <param name="data"></param>
    public void UploadAchor(byte[]data)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_UPLOAD_ANCHOR_C;
        model.Message = data;
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ����ê��
    /// </summary>
    public void DownloadAchor()
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_DOWNLOAD_ANCHOR_C;
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ������Ϣ��������������
    /// </summary>
    /// <param name="data"></param>
    public void SendMessageToRoomAll(string methodName,object[]para=null)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_SEND_ALL;
        OrderInfo method = new OrderInfo();
        method.MethodName = methodName;
        method.Paramerer = para;
        model.Message = DataCodeC.tobyteArray(method);
        UnityNetworkManager.Instance.Send(model);
    }
    /// <summary>
    /// ������Ϣ��������������
    /// </summary>
    /// <param name="data"></param>
    public void SendMessageToRoomOther(string methodName, object[] para = null)
    {
        DataModel model = new DataModel();
        model.Type = DataType.TYPE_SPORDER;
        model.Reequest = DataRequest.ROOM_SEND_OTHER;
        OrderInfo method = new OrderInfo();
        method.MethodName = methodName;
        method.Paramerer = para;
        model.Message = DataCodeC.tobyteArray(method);
        UnityNetworkManager.Instance.Send(model);
    }

    //ͬ����������Ϣ
    public void SendRobotInfoToRoomOther()
    {
        //a���������
        var a=RobotController.Instance;
        for (int i = 0; i < a.GetComponentsInChildren<Transform>(true).Length-1; i++)
         {
            DataModel model = new DataModel();
            model.Type = DataType.TYPE_SPORDER;
            model.Reequest = DataRequest.ROBOT_TRANS_INFO;
            RobotTransInfo robotTransInfo=new RobotTransInfo();
            var bb=a.GetComponentsInChildren<Transform>(true)[i];
            robotTransInfo.name=a.GetComponentsInChildren<Transform>(true)[i].name;
            robotTransInfo.posx=bb.localPosition.x;
            robotTransInfo.posy=bb.localPosition.y;
            robotTransInfo.posz=bb.localPosition.z;
            robotTransInfo.rotx=bb.localEulerAngles.x;
            robotTransInfo.roty=bb.localEulerAngles.y;
            robotTransInfo.rotz=bb.localEulerAngles.z;
            model.Message = DataCodeC.tobyteArray(robotTransInfo);
            UnityNetworkManager.Instance.Send(model);
         }
    }

}
