using UnityEngine;
using Random = UnityEngine.Random;


public class CutTest : MonoBehaviour
{
    public GameObject cube;//对象

    public Bounds mainBound;//包围盒
    private Octree tree;//树

    private bool startEnd = false;//控制结束
    public Camera cam;//相机
    private Plane[] _planes;//存储视锥体六个面

    void Start()
    {
        _planes = new Plane[6];//初始化
        int sideX = (int)mainBound.extents.x;
        int sideY = (int)mainBound.extents.y;
        int sideZ = (int)mainBound.extents.z;
        Bounds bounds = new Bounds(transform.position, new Vector3(sideX, sideY, sideZ));//生成包围盒
        tree = new Octree(bounds);//初始化行为树


        for (int x = -sideX/2; x < sideX/2; x+=10)//随机生成对象放到树中
        {
            for (int z = -sideZ/2; z < sideZ/2; z+=10)
            {
                for (int y = -sideY/2; y < sideY/2; y+=10)
                {
                    if (Random.Range(0, 20) < 1)
                    {
                        GameObject c = Instantiate(cube, transform);
                        c.transform.localScale = new Vector3(Random.Range(5, 10), Random.Range(5, 10), Random.Range(5, 10));
                        c.transform.position = new Vector3(x, y, z);
                        c.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

                        tree.Insert(new ObjData(c, c.transform.position, c.transform.eulerAngles));
                    }
                }

            }
        }

        startEnd = true;

    }


    void Update()
    {
        if (startEnd)
        {
            //获取摄像机视锥体六个面 
            GeometryUtility.CalculateFrustumPlanes(cam, _planes);
            // 更新_planes的数值
            tree.TriggerMove(_planes);//传六个面
        }
    }

    private void OnDrawGizmos()
    {
        if (startEnd)
        {
            tree.DrawBound();//开始绘制包围盒 用树组绘制盒
        }
        else
        {
            //初始化盒体
            //使用 center 和 size 绘制一个线框盒体。
            //mainBound.center中心节点   盒体大小
            Gizmos.DrawWireCube(mainBound.center, mainBound.size);
        }
    }
}