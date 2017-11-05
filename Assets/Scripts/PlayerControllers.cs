using InControl;
using UnityEngine;

public static class PlayerControllers {

    private static InputDevice player1 = null, player2 = null, player3 = null, player4 = null;
    private static Color player1Color, player2Color, player3Color, player4Color;

    public static void reset()
    {
        player1 = null;
        player2 = null;
        player3 = null;
        player4 = null;
    }

    public static int numOfPlayers()
    {
        if (player4 != null)
            return 4;
        if (player3 != null)
            return 3;
        if (player2 != null)
            return 2;
        if (player1 != null)
            return 1;
        return 0;
    }

    public static InputDevice getPlayerController(int i)
    {
        switch(i)
        {
            case 1: return player1;
            case 2: return player2;
            case 3: return player3;
            case 4: return player4;
            default: return null;
        }
    }

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

    public static Color Color1
    {
        get
        {
            return player1Color;
        }
        set
        {
            player1Color = value;
        }
    }

    public static Color Color2
    {
        get
        {
            return player2Color;
        }
        set
        {
            player2Color = value;
        }
    }

    public static Color Color3
    {
        get
        {
            return player3Color;
        }
        set
        {
            player3Color = value;
        }
    }

    public static Color Color4
    {
        get
        {
            return player4Color;
        }
        set
        {
            player4Color = value;
        }
    }
}
