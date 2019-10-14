using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Constant
{
    class ApiUrl
    {
        public const string API_BASE_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";

        public const string LOGIN_URL = " https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication ";

        public const string GET_INFORMATION_URL = " https://2-dot-backup-server-003.appspot.com/_api/v2/members/members/information";

        public const string GET_MINE_SONG_URL = " https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-mine ";

        public const string API_SONG = " https://2-dot-backup-server-003.appspot.com/_api/v2/songs ";
    }
}
