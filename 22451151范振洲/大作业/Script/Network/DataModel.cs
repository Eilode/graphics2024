

public class DataModel 
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public byte Type { get; set; }
    /// <summary>
    /// ��������
    /// </summary>
    public byte Reequest { get; set; }
    /// <summary>
    /// ��Ϣ��
    /// </summary>
    public byte[] Message { get; set; }
    
    public DataModel(byte type,byte request,byte[]message)
    {
        Type = type;
        Reequest = request;
        Message = message;
    }

    public DataModel()
    {

    }
}
