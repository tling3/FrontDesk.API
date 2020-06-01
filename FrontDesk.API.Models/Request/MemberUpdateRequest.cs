using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.API.Models.Request
{
    public class MemberUpdateRequest : MemberInsertRequest
    {
        public int Id { get; set; }
    }
}
