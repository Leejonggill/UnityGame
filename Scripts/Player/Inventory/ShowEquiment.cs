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

    //void OnEnable() // �̰� �κ��丮���Կ��� �����Ҷ� ���Ž�Ű�� �ؾߵ�.
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (PlayerStateManager.Instance.player[0].itesms[i] != null)
    //            slots[i].AddItem(PlayerStateManager.Instance.player[0].itesms[i]);
    //    }
    //}

    // �ƴϸ� ������Ʈ�� ������
    // �����꽽���� �κ��丮�����̳� ���⿡�� ���θ�����.
    // �÷��̾� �������� ������Ű���. ������ �Ϸ�.


    // �ؾ��Ұ�. ��主�� �ٸ������۵�
    // �÷��̾���Ͽ��ٵ� ������Ѿߵ�
    // ��������������J�µ� �� ��������� �������� �ٲ��.
    // �׸��� ����� ��� ������ε��ִµ�
    // ����� �����ιٲٰ� �������ϸ�
    // �κ��丮���Կ� ���� �ؾߵ�. Remove���� �����.

    public void SetEquimentSlots()
    {
        PlayerStateManager.Instance.LoadEquimentItem(0);
        PlayerStateManager.Instance.LoadEquimentItem(1);
        for (int i=0; i<slots.Length;i++)
        {
            slots[i].RemoveSlot(); // �̰� �ȳ����� OnEnable �� �÷��̾�1 ��������ϰ� �÷��̾�2 ���â���ιٲ۵� ���â������Ű��
            // �÷��̾�2 ���â�� �÷��̾�1 ���â ���簡��.
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
