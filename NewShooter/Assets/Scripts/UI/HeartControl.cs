using UnityEngine;
using UnityEngine.UI; // 使用正确的命名空间

public class HeartControl : MonoBehaviour
{
    public Slider heartSlider; // Inspector 里拖拽赋值
    private MyPlayerHealth playerHealth; // 玩家健康脚本引用
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerHealth>();
    }
    private void Start()
    {
        heartSlider.value = 10; // 初始化心形滑块的值

    }

    private void Update()
    {
        heartSlider.value = playerHealth.startHealth; // 更新心形滑块的值为玩家当前的生命值

    }
}