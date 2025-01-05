using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoneAllModelController : MonoBehaviour
{
    public static StoneAllModelController Instance;
    //-2����û������ֱ�Ӵ洢���ӱ��(��Ϊ���������0)
    public int[][] Qi_Pan=new int[9][];
    public List<GameObject> Model;
    //�������̶�λ��İ���
    [Tooltip("�������̶�λ��İ���")]
    public GameObject cube;
    public Transform[][] Qi_Pan_Pos = new Transform[9][];
    [Tooltip("�������ӵ��ƶ��ٶ� 0.01Ϊ����1Ϊ����")]
    public float speed = 0.01f;

    private void Awake()
    {
        Instance = this;
        //��ʼ���ܵĶ�ά����
        for(int i=0;i<9;i++)
        {
            Qi_Pan[i] = new int[10];
            for(int j=0;j<10;j++)
            {
                Qi_Pan[i][j] = -2;
            }
        }
        Init_Qi_Pan_Pos();
  

    }

    //��ʼ�����̶�λ��
    void Init_Qi_Pan_Pos()
    {
        
        int a = 0;
        var pan = GameObject.Find("3D����");
        //���ٿռ�
        for(int i=0;i<9;i++)
        {
            Qi_Pan_Pos[i] = new Transform[10];
        }
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<9;j++)
            {
                Qi_Pan_Pos[j][i] = pan.transform.GetChild(a);
                a++;
            }
        }
        print(Qi_Pan_Pos[0][0].position);
    }
    //��ʼ�����ӵ�λ��
    void 
    Init_Stone_pos()
    {
        for(int i=0;i<Model.Count;i++)
        {
            Model[i].GetComponent<NavMeshAgent>().enabled = false;
            //��ö�Ӧλ������
            int x = (int)Model[i].GetComponent<StoneModelManager>().pos.x;
            int y = (int)Model[i].GetComponent<StoneModelManager>().pos.y;
            Model[i].transform.position = new Vector3(Qi_Pan_Pos[x][y].position.x, Model[i].transform.position.y, Qi_Pan_Pos[x][y].position.z);
           
        }
        
    }

        //������ģ�����ݵĳ�ʼ������
        void Init()
        {
            //�ҵ�����ģ��
            GameObject oo = GameObject.Find("����ģ��");
            for (int i = 0; i < oo.transform.childCount; i++)
            {
                Model.Add(oo.transform.GetChild(i).gameObject);
                //��ӱ��
                oo.transform.GetChild(i).gameObject.GetComponent<StoneModelManager>().id = i;
            }
            //��ģ�͵�λ�ñ����
            for (int i = 0; i < Model.Count; i++)
            {
                if (Model[i].GetComponent<StoneModelManager>()._red)
                    Qi_Pan[(int)Model[i].GetComponent<StoneModelManager>().pos.x][(int)Model[i].GetComponent<StoneModelManager>().pos.y] = i;
                else
                    Qi_Pan[(int)Model[i].GetComponent<StoneModelManager>().pos.x][(int)Model[i].GetComponent<StoneModelManager>().pos.y] = i;
            }
        }

    private void Start()
    {
        Init();
        Init_Stone_pos();
    }




}
