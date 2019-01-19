using System;

namespace Imperium.Core.Systems.Order
{
    public interface IOrder
    {
        TimeSpan Update(TimeSpan maxTime);
    }
}