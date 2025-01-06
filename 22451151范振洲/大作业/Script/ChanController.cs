using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ChanController : MonoBehaviour
{
    public GameObject l_foot, r_foot, l_knee, r_knee, l_leg, r_leg, l_hand, r_hand, l_elbow, r_elbow, l_arm, r_arm, head;
    //��¼�ؽڵ�����
    public Vector2[] data;
    //�ؽڵ����
    int num = 13;
    //��¼�Ƿ�ʼʶ��
    public bool is_begin = false;
    //��¼ÿ���ؽڵ��Ƿ�ʼ����¼
    public bool[] data_bool = { false };
    //��¼��ǰ��ȡ��json�ļ����
    int num_json = 1;
    //��ȡ���ݵ��ٶ�
    public int speed = 1;
    //�ж������Ƿ�ʼ����ʶ��
    [InspectorButton]
    public bool if_begin()
    {
        print(data_bool[0]);
        string a = @"E:\openpose-1.7.0\output\000000000000_keypoints.json";
        // @"E:\openpose-1.7.0\output\000000000000_keypoints.json";
        try
        {
            //��������
            string jsonTest = File.ReadAllText(a, Encoding.UTF8);
            ModelTest obj = JsonUtility.FromJson<ModelTest>(jsonTest);
            for (int i = 0; i < 18; i++)
            {
                print("��" + i + "�����x����= " + obj.people[0].pose_keypoints_2d[3 * i]);
                print("��" + i + "�����y����= " + obj.people[0].pose_keypoints_2d[3 * i + 1]);
                data[i] = new Vector2(obj.people[0].pose_keypoints_2d[3 * i], obj.people[0].pose_keypoints_2d[3 * i + 1]);


            }
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    public ModelTest TryFigure(int q)
    {
        string a = @"E:\openpose-1.7.0\output\0000" + q.ToString("00000000") + "_keypoints.json";
        try
        {
            //��������
            string jsonTest = File.ReadAllText(a, Encoding.UTF8);
            ModelTest obj = JsonUtility.FromJson<ModelTest>(jsonTest);
            for (int i = 0; i < 18; i++)
            {
                print("��" + i + "�����x����= " + obj.people[0].pose_keypoints_2d[3 * i]);
                print("��" + i + "�����y����= " + obj.people[0].pose_keypoints_2d[3 * i + 1]);
                data[i] = new Vector2(obj.people[0].pose_keypoints_2d[3 * i], obj.people[0].pose_keypoints_2d[3 * i + 1]);
                num_json += speed;
            }
            return obj;
        }
        catch (ArgumentOutOfRangeException)
        {
            num_json = -1;
            return null;
        }
    }

    private void Awake()
    {
        data = new Vector2[18];
    }

    #region ��֫�岻ͬλ�õ���ת�ж�
    //�Բ���������ת���ж�
    public void Neck_LR_R()
    {
        //�������жϵĵ��ж�ʧ��ʱ�򷵻�
        if (data[0] == new Vector2(0, 0) || data[1] == new Vector2(0, 0)) return;
        double tan = (data[0].x - data[1].x) / (data[0].y - data[1].y);
        double tanRadianValue2 = Math.Atan(tan);//�󻡶�ֵ
        double tanAngleValue2 = tanRadianValue2 / Math.PI * 180;//��Ƕ�
        head.transform.localEulerAngles = new Vector3(head.transform.localEulerAngles.x, (float)tanAngleValue2,head.transform.localEulerAngles.z);
    }

    //�Ҹ첲�Ŀ���
    public void L_arm_R()
    {
        //�������жϵĵ��ж�ʧ��ʱ�򷵻�
        if (data[2] == new Vector2(0, 0) || data[3] == new Vector2(0, 0)) return;
        //�ֱ�����ʱ
        if (data[2].y < data[3].y)
        {
            double tan = (data[3].y - data[2].y) / (data[2].x - data[3].x);
            double tanRadianValue2 = Math.Atan(tan);//�󻡶�ֵ
            double tanAngleValue2 = tanRadianValue2 / Math.PI * 180;//��Ƕ�
            l_arm.transform.localEulerAngles = new Vector3(l_arm.transform.localEulerAngles.x, (float)tanAngleValue2, l_arm.transform.localEulerAngles.z);

        }
        //�ֱ�����
        else
        {
            double tan = (data[2].y - data[3].y) / (data[2].x - data[3].x);
            double tanRadianValue2 = Math.Atan(tan);//�󻡶�ֵ
            double tanAngleValue2 = tanRadianValue2 / Math.PI * 180;//��Ƕ�
            l_arm.transform.localEulerAngles = new Vector3(l_arm.transform.localEulerAngles.x, -(float)tanAngleValue2, l_arm.transform.localEulerAngles.z);
        }
    }

    //��첲�Ŀ���
    public void R_arm_R()
    {
        //�������жϵĵ��ж�ʧ��ʱ�򷵻�
        if (data[5] == new Vector2(0, 0) || data[6] == new Vector2(0, 0)) return;
        //�ֱ�����ʱ
        if (data[5].y < data[6].y)
        {
            double tan = (data[6].y - data[5].y) / (data[6].x - data[5].x);
            double tanRadianValue2 = Math.Atan(tan);//�󻡶�ֵ
            double tanAngleValue2 = tanRadianValue2 / Math.PI * 180;//��Ƕ�
            r_arm.transform.localEulerAngles = new Vector3(r_arm.transform.localEulerAngles.x, -(float)tanAngleValue2, r_arm.transform.localEulerAngles.z);

        }
        //�ֱ�����
        else
        {
            double tan = (data[5].y - data[6].y) / (data[6].x - data[5].x);
            double tanRadianValue2 = Math.Atan(tan);//�󻡶�ֵ
            double tanAngleValue2 = tanRadianValue2 / Math.PI * 180;//��Ƕ�
            r_arm.transform.localEulerAngles = new Vector3(r_arm.transform.localEulerAngles.x, (float)tanAngleValue2, r_arm.transform.localEulerAngles.z);
        }
    }

    //���ָ첲�����
    public void Elbow_R_R()
    {
        //�첲��������4��xС��2 ��ͬ�����Ҫ���ֱ۽��з�ת
        if (data[2] == new Vector2(0, 0) || data[3] == new Vector2(0, 0) || data[4] == new Vector2(0, 0)) return;
        if (data[4].y > data[3].y)
        {
            double a = Vector2.Distance(data[4], data[2]);//�Ա�
            double b = Vector2.Distance(data[3], data[4]);
            double c = Vector2.Distance(data[3], data[2]);
            //�Ƕȵ�����ֵ
            double cos_ang1 = (b * b + c * c - a * a) / (2 * b * c);
            //����
            double cosRadian = Math.Acos(cos_ang1);
            //ת�Ƕ�
            double tanAngleValue2 = 180 - (cosRadian / Math.PI * 180);//��Ƕ�
            l_elbow.transform.localEulerAngles = new Vector3(0f, (float)tanAngleValue2,l_elbow.transform.localEulerAngles.z);
        }
        //�첲��������4��x����2
        else
        {
            //260
            double a = Vector2.Distance(data[4], data[2]);//�Ա�
            double b = Vector2.Distance(data[3], data[4]);
            double c = Vector2.Distance(data[3], data[2]);
            //�Ƕȵ�����ֵ
            double cos_ang1 = (b * b + c * c - a * a) / (2 * b * c);
            //����
            double cosRadian = Math.Acos(cos_ang1);
            //ת�Ƕ�
            double tanAngleValue2 = 180 - (cosRadian / Math.PI * 180);//��Ƕ�
            l_elbow.transform.localEulerAngles = new Vector3(-119f, -(float)tanAngleValue2, l_elbow.transform.localEulerAngles.z);
        }
    }

    //���ָ첲��Ŀ���
    public void Elbow_L_R()
    {
        //�첲��������4��xС��2 ��ͬ�����Ҫ���ֱ۽��з�ת
        if (data[5] == new Vector2(0, 0) || data[6] == new Vector2(0, 0) || data[7] == new Vector2(0, 0)) return;
        if (data[7].y > data[6].y)
        {
            double a = Vector2.Distance(data[5], data[7]);//�Ա�
            double b = Vector2.Distance(data[5], data[6]);
            double c = Vector2.Distance(data[6], data[7]);
            //�Ƕȵ�����ֵ
            double cos_ang1 = (b * b + c * c - a * a) / (2 * b * c);
            //����
            double cosRadian = Math.Acos(cos_ang1);
            //ת�Ƕ�
            double tanAngleValue2 = 180 - (cosRadian / Math.PI * 180);//��Ƕ�
            r_elbow.transform.localEulerAngles = new Vector3(r_elbow.transform.localEulerAngles.x, 0f, -(float)tanAngleValue2);
        }
        //�첲��������4��x����2
        else
        {
            //260
            double a = Vector2.Distance(data[5], data[7]);//�Ա�
            double b = Vector2.Distance(data[5], data[6]);
            double c = Vector2.Distance(data[6], data[7]);
            //�Ƕȵ�����ֵ
            double cos_ang1 = (b * b + c * c - a * a) / (2 * b * c);
            //����
            double cosRadian = Math.Acos(cos_ang1);
            //ת�Ƕ�
            double tanAngleValue2 = 180 - (cosRadian / Math.PI * 180);//��Ƕ�
            r_elbow.transform.localEulerAngles = new Vector3(r_elbow.transform.localEulerAngles.x, -145f, -(float)tanAngleValue2);
        }
    }

    #endregion

    //�����ݸ��µļ��Ϻ���
    public void Update_body_data()
    {
        TryFigure(num_json);
        Neck_LR_R();
        L_arm_R();
        R_arm_R();
        Elbow_R_R();
        Elbow_L_R();
    }

    private void Update()
    {
        if (if_begin())
        {
            //�ж϶�ȡ���ļ�û�г�����Χ
            if (num_json != -1)
            {
                Update_body_data();
            }
        }
    }
}
