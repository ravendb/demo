public static class DiscountUtils
{
    public static decimal CalcSalePrice(decimal price)
    {
        return price - price / 100M * 25M;
    }
    
    public static decimal CalcProfitPrice(decimal price)
    {
        return price + price / 100M * 25M;
    }
}
