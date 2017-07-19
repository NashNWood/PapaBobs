using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobs.Domain
{
    public class OrderManager
    {
        public static void CreateOrder(DTO.OrderDTO orderDTO)
        {
            // Validation

            if (orderDTO.Name.Trim().Length == 0)
                throw new Exception("Please enter a name.");

            if (orderDTO.Address.Trim().Length == 0)
                throw new Exception("Please enter an address");

            if (orderDTO.Zip.Trim().Length == 0)
                throw new Exception("please enter a zip code.");

            if (orderDTO.Phone.Trim().Length == 0)
                throw new Exception("Please enter a phone number.");

            orderDTO.OrderId = Guid.NewGuid();
            orderDTO.TotalCost = PizzaPriceManager.CalculateCost(orderDTO);
            Persistence.OrderRepository.CreateOrder(orderDTO);
        }

        public static void CompleteOrder(Guid orderId)
        {
            Persistence.OrderRepository.CompleteOrder(orderId);
        }

        public static object GetOrders()
        {
            return Persistence.OrderRepository.GetOrders();
        }
    }
}
