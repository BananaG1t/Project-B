static class Order
{
    public static OrderModel SelectOrder(AccountModel account)
    {
        return OrderLogic.SelectOrder(account);
    }
}