using Assignment2_UWP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Service
{
    interface MemberService
    {
        // Thực hiện các chức năng liên quan đến member bao gồm:



        // nhận tham số  đầu vào là, ra là..., có validate.

        String Login(String username, String password);



        MemberRegister Register(MemberRegister member);





        MemberRegister GetInformation(String token);
    }
}
