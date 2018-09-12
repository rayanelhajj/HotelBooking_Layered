﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Models;

namespace HotelBooking.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly HotelBookingContext db;

        public CustomerRepository()
        {
            db = new HotelBookingContextFactory().Context;
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
