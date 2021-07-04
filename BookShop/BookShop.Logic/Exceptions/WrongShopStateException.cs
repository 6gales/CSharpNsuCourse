using System;

namespace BookShop.Logic.Exceptions
{
    public sealed class WrongShopStateException : Exception
    {
        public WrongShopStateException(int expectedId, int providedId) : base(
            $"Expected ShopState of {expectedId}, provided {providedId}")
        {
        }
    }
}