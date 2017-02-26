using System;
using System.Collections.Generic;

namespace MoveManager.Business
{
    public class Model
    {
        public class Agent
        {
            public Agent()
            {
                Appointments = new Dictionary<long, Appointment>();
            }

            public Guid AgentId { get; set; }
            public string Name { get; set; }
            public Dictionary<long, Appointment> Appointments { get; set; }
        }

        public class Appointment
        {
            public Guid CustomerId { get; set; }
            public Guid AgentId { get; set; }
            public long AppointmentId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public Address Address { get; set; }
        }

        public class Survey
        {
            public Guid CustomerId { get; set; }
            public long MoveId { get; set; }
            public long SurveyId { get; set; }
        }

        public class Address
        {
            public string Country { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
        }

        public class Customer
        {
            public Customer()
            {
                Moves = new Dictionary<long, Move>();
            }

            public Guid CustomerId { get; set; }
            public string Name { get; set; }
            public Dictionary<long, Move> Moves { get; set; }
            public Address Address { get; set; }
        }

        public class Move
        {
            public Guid CustomerId { get; set; }
            public long MoveId { get; set; }
            public Survey Survey { get; set; }
        }
    }
}