﻿using System;
using Core.Domain;

namespace Account.Domain
{
    public class AccountRegistered: IEvent
    {
        public AccountRegistered(string id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public string Id { get; }
        public string Username { get; }
        public string Password { get; }
    }
}
