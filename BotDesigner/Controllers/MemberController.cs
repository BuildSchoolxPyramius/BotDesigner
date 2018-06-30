using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;

namespace BotDesigner.Controllers
{
    public class MemberController : ApiController
    {

        
        //取得所有會員資料
        public static List<Member> GetAllMembersInfo()
        {
            DataClasses1DataContext DB = new DataClasses1DataContext();
            List<Member> List_Members = new List<Member>();
            var T_Members = DB.Members;
            foreach (var item in T_Members)
            {
                List_Members.Add(new Member { MemberID = item.MemberID, Address = item.Address, Name = item.Name , Cellphone = item.Phone});
            }
            return List_Members;
        }



        //呼叫方法來取得所有會員資料
        [ActionName("all")]
        [HttpPost]
        public JObject GetAllMembers()
        {
            JObject json = new JObject();
            json["Members"] = JToken.FromObject(GetAllMembersInfo());

            return json;
        }

        //透過MemberID查詢會員資料
        [ActionName("no")]
        [HttpPost]
        public IHttpActionResult GetMember(int id)
        {
            var Member = GetAllMembersInfo().FirstOrDefault((p) => p.MemberID == id);
            if (Member == null)
            {
                return NotFound();
            }
            return Ok(Member);
        }


        //新增Member的一筆資料
        [ActionName("create")]
        [HttpPost]
        public IHttpActionResult CreateMember(JObject Jobject)
        {
            DataClasses1DataContext DB = new DataClasses1DataContext();
            var member = new BotDesigner.Members();
            member.Name = Jobject.Property("Name").Value.ToString();
            member.Address = Jobject.Property("Address").Value.ToString();
            member.Phone = Jobject.Property("Cellphone").Value.ToString();
            DB.Members.InsertOnSubmit(member);
            DB.SubmitChanges();
            return Ok(member);
        }


        //更改Member的一筆資料
        [ActionName("alter")]
        [HttpPost]
        public IHttpActionResult UpdateMemberByID(JObject Jobject)
        {
            DataClasses1DataContext DB = new DataClasses1DataContext();

            var user = DB.Members.Single(p => p.MemberID == (int)Jobject.Property("MemberID").Value);
            user.Name = Jobject.Property("Name").Value.ToString();
            user.Address = Jobject.Property("Address").Value.ToString();
            user.Account = Jobject.Property("Account").Value.ToString();
            user.Password = Jobject.Property("Password").Value.ToString();
            DB.SubmitChanges();

            return Ok(user);
        }

        [ActionName("delete")]
        [HttpPost]
        public IHttpActionResult DeleteMemberByID(int id)
        {
            DataClasses1DataContext DB = new DataClasses1DataContext();

            var DelItem = from p in DB.Members where p.MemberID.Equals(id) select p;
            DB.Members.DeleteAllOnSubmit(DelItem);
            DB.SubmitChanges();

            return Ok();
        }

        


    }
}
