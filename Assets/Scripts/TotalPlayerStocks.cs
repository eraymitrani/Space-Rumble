public static class TotalPlayerStocks {

    private static int[] stocksLeft = new int[4];

    public static void reset()
    {
        for(int i = 0; i < 4; i++)
        {
            stocksLeft[i] = 0;
        }
    }

    public static void addStocks(int playerNum, int stocks)
    {
        stocksLeft[playerNum - 1] += stocks;
    }

    public static int getPlayerStocks(int playerNum)
    {
        return stocksLeft[playerNum - 1];
    }
}
