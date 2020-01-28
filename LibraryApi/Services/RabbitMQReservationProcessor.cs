using LibraryApi.Models;
using RabbitMqUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class RabbitMQReservationProcessor : ISendMessagesToTheReservationProcessor
    {
        IRabbitManager Manager;

        public RabbitMQReservationProcessor(IRabbitManager manager)
        {
            Manager = manager;
        }

        public void SendForProcessing(GetReservationItemResponse response)
        {
            Manager.Publish(response, "", "direct", "reservationQueue");
        }
    }
}
