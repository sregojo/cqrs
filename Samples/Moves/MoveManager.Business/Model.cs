using System;
using System.Collections.Generic;
using System.Linq;

namespace MoveManager.Business
{
    public class Model
    {
        public class Agent
        {
            public readonly Dictionary<long, Appointment> Appointments = new Dictionary<long, Appointment>();
            public Guid AgentId { get; set; }
            public string Name { get; set; }

            public bool HasAppointment(long appointmentId)
            {
                return this.Appointments.ContainsKey(appointmentId);
            }

            public bool CanScheduleAppointment(DateTime startDate, DateTime endDate)
            {
                if (startDate >= endDate) return false;

                return !this.Appointments.Values.Any(a =>
                    startDate >= a.StartDate && startDate < a.EndDate
                    ||
                    endDate > a.StartDate && endDate <= a.EndDate);
            }
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
            public readonly Dictionary<long, Move> Moves = new Dictionary<long, Move>();
            public Guid CustomerId { get; set; }
            public string Name { get; set; }
            public Address Address { get; set; }
        }

        public interface ICall
        {
            string Type { get; }
            DateTime Date { get; }
        }

        public class CallCenter
        {
            public readonly Dictionary<Guid, ICall> Calls = new Dictionary<Guid, ICall>();
        }

        public class AppointmentCall : ICall
        {
            public Guid Id { get; set; }
            public Customer Customer { get; set; }
            public Appointment Appointment { get; set; }
            public string Type => "Appointment.New";
            public DateTime Date { get; set; }
        }

        public class Move
        {
            public Guid CustomerId { get; set; }
            public long MoveId { get; set; }
            public Survey Survey { get; set; }
        }
    }
}