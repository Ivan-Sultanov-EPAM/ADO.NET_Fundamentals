using DemoProject.Models;
using System;

namespace DemoProject.Helpers
{
    public static class ObjectExtensions
    {
        public static OrderStatus ToOrderStatus(this object status)
        {
            return status.ToString() switch
            {
                "NotStarted" => OrderStatus.NotStarted,
                "Loading" => OrderStatus.Loading,
                "InProgress" => OrderStatus.InProgress,
                "Arrived" => OrderStatus.Arrived,
                "Unloading" => OrderStatus.Unloading,
                "Cancelled" => OrderStatus.Cancelled,
                "Done" => OrderStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status))
            };
        }

        public static int ToInt(this object value)
        {
            if (int.TryParse(value.ToString(), out var result))
            {
                return result;
            }

            throw new ArgumentException(nameof(value));
        }

        public static DateTime ToDate(this object value)
        {
            if (DateTime.TryParse(value.ToString(), out var result))
            {
                return result;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}