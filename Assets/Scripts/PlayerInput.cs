using UnityEngine;

public static class PlayerInput
{

    public static int ThrottleUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        return 0;
    }

    public static bool Fire()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public static int Horizontal()
    {
        return (int)Input.GetAxisRaw("Horizontal");
    }
}
