using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_maker : MonoBehaviour
{
    public GameObject cube;
    public int dis = 2;

    [InspectorButton("���ɶ�λ��")]
    public void Make_cube()
    {
        var x = cube.transform.position.x;
        var y = cube.transform.position.z;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //�Ե�һ���Ѿ��ڳ����е�ģ�͵����⴦��
                if (i == 0 && j == 0)
                {
                    x-=dis;
                    continue;
                }
                var cube_new = GameObject.Instantiate(cube);
                cube_new.transform.position = new Vector3(x, cube.transform.position.y, y);

                cube_new.name = j.ToString() + i.ToString();
                cube_new.transform.parent = cube.transform.parent;
                x-=dis;
            }
            x = cube.transform.position.x;
            y-=dis;
        }
    }
}
