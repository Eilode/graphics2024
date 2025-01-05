using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static StoneManager;
using static UnityEngine.GraphicsBuffer;

public class StoneModelManager : MonoBehaviour
{
    public struct Stone
    {
        public enum TYPE { JIANG, CHE, PAO, MA, BING, SHI, XIANG };

        //���ӵ�ID
        public int _id;

        //�����Ǻ��ӻ��Ǻ���,IDС��16���Ǻ��ӣ�����16�Ǻ���
        public bool _red;

        //�����Ƿ�����
        public bool _dead;

        //�����������е�λ��,
        public Vector2 _pos;

        //���ӵ�����
        public TYPE _type;
       

        //���ӳ�ʼ��������32�����Ӷ�Ӧ�����Բ���
    }
    public Stone stone;
    public Vector2 pos;
    public bool _red;
    //��¼�����ڹ������ı��
    public int id;
    //���Բ���
    public int a, b;
    bool _move, attack, _dead;
    GameObject move_target;
    //ƽ���˶�����
    float t1 = 0;
    private  bool is_move_no_target = false;

    //�������ж��Լ����͵��Զ���
    void Init()
    {
        //��������ɫ���ж�
        stone._red = (name[0] == '��');
        stone._dead = false;
        string a = "";
       for(int i=1;i<name.Length;i++)
       {
                a += name[i];
        }
        

        //�Ժ���Ĵ���ͶԺ����Ԥ����ʵ�ֶ����λ�ó�ʼ��
        switch(a)
        {
            case "��1":
                stone._pos = new Vector2(0f, 0f);
                break;
            case "��1":
                stone._pos = new Vector2(1f, 0f);
                break;
            case "��1":
                stone._pos = new Vector2(2f, 0f);
                break;
            case "ʿ1":
                stone._pos = new Vector2(3f, 0f);
                break;
            case "��1":
                stone._pos = new Vector2(4f, 0f);
                break;
            case "ʿ2":
                stone._pos = new Vector2(5f, 0f);
                break;
            case "��2":
                stone._pos = new Vector2(6f, 0f);
                break;
            case "��2":
                stone._pos = new Vector2(7f, 0f);
                break;
            case "��2":
                stone._pos = new Vector2(8f, 0f);
                break;
            case "��1":
                stone._pos = new Vector2(1f, 2f);
                break;
            case "��2":
                stone._pos = new Vector2(7f, 2f);
                break;
            case "��1":
                stone._pos = new Vector2(0f, 3f);
                break;
            case "��2":
                stone._pos = new Vector2(2f, 3f);
                break;
            case "��3":
                stone._pos = new Vector2(4f, 3f);
                break;
            case "��4":
                stone._pos = new Vector2(6f, 3f);
                break;
            case "��5":
                stone._pos = new Vector2(8f, 3f);
                break;
        }
        //�Ժ���ĶԳƴ���
        if(stone._red)
        {
            stone._pos = new Vector2(stone._pos.x, 9 - stone._pos.y);
        }
        _red = stone._red;
    }

    //�ƶ����ӵĺ���
    [InspectorButton("�ƶ�����")]
    public void movetest()
    {
        Move(a, b);
    }
    public void Move(int a,int b)
    {
        int x = (int)(pos.x + a);
        int y = (int)(pos.y + b);
        print(x + "  " + y);
        //�ж��ƶ�����û������
        if (StoneAllModelController.Instance.Qi_Pan[x][y]==-2)
        {
            StoneAllModelController.Instance.Qi_Pan[x][y] = id;
            StoneAllModelController.Instance.Qi_Pan[(int)pos.x][(int)pos.y] = -2;
            pos = new Vector3(x, y);
            //to do ��ʵ�ƶ���������
            move_target = StoneAllModelController.Instance.Qi_Pan_Pos[x][y].gameObject;
            is_move_no_target = true;
            this.GetComponent<Animator>().SetTrigger("walk");
            this.GetComponent<Animator>().ResetTrigger("idle");
            Move_To_point();
        }
        else 
        {
            //�����ƶ���λ�����Ǽ���������
            if (StoneAllModelController.Instance.Model[StoneAllModelController.Instance.Qi_Pan[x][y]].GetComponent<StoneModelManager>()._red==_red)
            {
                print("�޷��ƶ���λ���Ǽ������ӣ�" + StoneAllModelController.Instance.Model[StoneAllModelController.Instance.Qi_Pan[x][y]].name);
            }
            //�ǶԷ�����
            else
            {
                var agent = GetComponent<NavMeshAgent>();
                var target = StoneAllModelController.Instance.Model[StoneAllModelController.Instance.Qi_Pan[x][y]];
                transform.LookAt(target.transform);
                //this.GetComponent<NavMeshAgent>().destination=target.transform.position;
                this.GetComponent<Animator>().SetTrigger("walk");
                this.GetComponent<Animator>().ResetTrigger("idle");
                _move = true;
                move_target = target;
                StoneAllModelController.Instance.Qi_Pan[(int)pos.x][(int)pos.y] = -2;
                pos = new Vector3(x, y);
                StoneAllModelController.Instance.Qi_Pan[(int)pos.x][(int)pos.y] = id;
            }
        }
    }

    //������û������ʱ�����ƶ�
    private void Move_To_point()
    {
        if (!is_move_no_target) return;
        transform.LookAt(move_target.transform);
        t1 += Time.deltaTime * StoneAllModelController.Instance.speed;
        //ͨ���ٶȿ��ƶ���
        GetComponent<Animator>().speed =Mathf.Lerp(GetComponent<Animator>().speed, 2.0f, t1);
        transform.position = Vector3.Lerp(transform.position, move_target.transform.position, t1);
        if (Vector3.Distance(move_target.transform.position, transform.position) < 0.5f)
        {
            is_move_no_target = false;
            GetComponent<Animator>().speed = 1f;
            this.GetComponent<Animator>().ResetTrigger("walk");
            this.GetComponent<Animator>().SetTrigger("idle");
            t1 = 0f;
        }
    }

    private void Awake()
    {
        Init();
        pos = stone._pos;
        _move= attack= _dead = false;
    }

    //�Թ������������������ж�����
    void MovingToTarget()
    {
        if(_move)
        {
            t1 += Time.deltaTime*StoneAllModelController.Instance.speed;
            //ͨ���ٶȿ��ƶ���
            GetComponent<Animator>().speed = Mathf.Lerp(GetComponent<Animator>().speed, 2.0f, t1);
            transform.position = Vector3.Lerp(transform.position, move_target.transform.position, t1);
            //ƽ���˶�
            if (Vector3.Distance(move_target.transform.position,transform.position)<1f)
        {
                GetComponent<Animator>().speed = 1f;
                _move = false;
            attack = true;
                this.GetComponent<Animator>().ResetTrigger("attack");
            this.GetComponent<Animator>().SetTrigger("attack");
             t1 = 0f;
        }
        }
    }
    //�ؼ�֡ʹ�õ�ɱ�����ӵĺ���
    public void kill_Stone()
    {
        move_target.GetComponent<StoneModelManager>()._dead = true;
        move_target.GetComponent<Animator>().SetTrigger("dead");
        move_target = null;
        GetComponent<Animator>().ResetTrigger("attack");
        GetComponent<Animator>().SetTrigger("idle");
    }
    //�����ؼ�֡����
    public void dead()
    {
        //to do 
        //����������ʧ����������ͬ��
        this.gameObject.SetActive(false);
    }
    //���Լ�λ�õĸ���
    [InspectorButton("����λ��")]
    public void UpDdate_Pos()
    {
        var par = transform.parent;
        transform.parent = StoneAllModelController.Instance.Qi_Pan_Pos[(int)pos.x][(int)pos.y];
        transform.localPosition = new Vector3(0f, transform.localPosition.y,0f);
        
        
    }
    private void Update()
    {
        MovingToTarget();
        Move_To_point();
    }

}
