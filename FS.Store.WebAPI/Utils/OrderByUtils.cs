using FS.Store.WebAPI.Models;

namespace FS.Store.WebAPI.Utils
{
    public static class OrderByUtils
    {
        private static readonly List<OrderBy> Orders =
        [
            new(){ Id = 0, Text = null, Icon = "bi-arrow-down-up" },
            new(){ Id = 1, Text = "asc", Icon = "bi-sort-alpha-down" },
            new(){ Id = 2, Text = "desc", Icon = "bi-sort-alpha-down-alt" }
        ];

        private static readonly List<OrderBy> OrdersNumber =
        [
            new(){ Id = 0, Text = null, Icon = "bi-arrow-down-up" },
            new(){ Id = 1, Text = "asc", Icon = "bi-sort-numeric-down" },
            new(){ Id = 2, Text = "desc", Icon = "bi-sort-numeric-down-alt" }
        ];

        public static OrderBy GetOrderBy(int orderId, bool number = false)
        {
            var orders = number ? OrdersNumber : Orders;

            return orders.FirstOrDefault(o => o.Id == orderId) ?? orders[0];
        }

        public static OrderBy GetOrderBy(string? orderBy, bool number = false)
        {
            if (number)
                return OrdersNumber.FirstOrDefault(o => string.Equals(o.Text, orderBy, StringComparison.OrdinalIgnoreCase)) ?? new();

            return Orders.FirstOrDefault(o => string.Equals(o.Text, orderBy, StringComparison.OrdinalIgnoreCase)) ?? new();
        }

        public static OrderBy GetNextOrderBy(int orderId, bool number = false)
        {
            int nextId = (orderId + 1) % 3;

            return GetOrderBy(nextId, number);
        }
        public static OrderBy GetNextOrderBy(string orderBy, bool number = false)
        {
            int orderId = GetOrderBy(orderBy, number)?.Id ?? 0;
            return GetNextOrderBy(orderId, number);
        }
    }
}
