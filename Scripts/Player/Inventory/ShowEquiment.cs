using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEquiment : MonoBehaviour
{
    public InventorySlot[] slots;
    public Inventory inventory;

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<InventorySlot>();
        inventory = Inventory.Instance;
    }

    private void OnEnable()
    {
        SetEquimentSlots();
    }

    //void OnEnable() // 이걸 인벤토리슬롯에서 착용할때 갱신시키게 해야됨.
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (PlayerStateManager.Instance.player[0].itesms[i] != null)
    //            slots[i].AddItem(PlayerStateManager.Instance.player[0].itesms[i]);
    //    }
    //}

    // 아니면 업데이트로 돌린후
    // 리무브슬롯을 인벤토리슬롯이나 여기에다 새로만든후.
    // 플레이어 아이템을 설정시키면됨. 갱신은 완료.


    // 해야할것. 헬멧말고도 다른아이템들
    // 플레이어메일에다도 적용시켜야됨
    // 아이템헬멧을껴놧는데 또 헬멧을끼면 아이템이 바뀌게.
    // 그리고 헬멧을 사용 버리기로되있는데
    // 헬멧을 해제로바꾸고 해제를하면
    // 인벤토리슬롯에 들어가게 해야됨. Remove새로 만들기.

    public void SetEquimentSlots()
    {
        PlayerStateManager.Instance.LoadEquimentItem(0);
        PlayerStateManager.Instance.LoadEquimentItem(1);
        for (int i=0; i<slots.Length;i++)
        {
            slots[i].RemoveSlot(); // 이걸 안넣으면 OnEnable 때 플레이어1 장비를장착하고 플레이어2 장비창으로바꾼뒤 장비창을껏다키면
            // 플레이어2 장비창이 플레이어1 장비창 복사가됨.
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (InventoryKey.SelectKey == 1)
            {
                if (PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[i] != null)
                    slots[i].AddItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[i]);
            }
            else if (InventoryKey.SelectKey == 2)
            {
                if (PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[i] != null)
                    slots[i].AddItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[i]);
            }
        }
    }
}
