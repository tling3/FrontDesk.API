﻿using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMemberRepo
    {
        bool SaveChanges();
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member> GetMemberById(int id);
        Task InsertMember(Member member);
        void UpdateMember(Member memberModel);
    }
}
