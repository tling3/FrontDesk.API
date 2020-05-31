using FrontDesk.API.Models.Request.Add;

namespace FrontDesk.API.Models.Request.Update
{
    public class MemberUpdateRequest : MemberAddRequest
    {
        public int Id { get; set; }
    }
}
