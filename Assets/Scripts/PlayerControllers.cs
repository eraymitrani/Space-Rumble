using InControl;

public static class PlayerControllers {

    private static InputDevice player1 = null, player2 = null, player3 = null, player4 = null;

    public static InputDevice Player1
    {
        get
        {
            return player1;
        }
        set
        {
            player1 = value;
        }
    }

    public static InputDevice Player2
    {
        get
        {
            return player2;
        }
        set
        {
            player2 = value;
        }
    }

    public static InputDevice Player3
    {
        get
        {
            return player3;
        }
        set
        {
            player3 = value;
        }
    }

    public static InputDevice Player4
    {
        get
        {
            return player4;
        }
        set
        {
            player4 = value;
        }
    }
}
