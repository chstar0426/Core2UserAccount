using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsrAccount.Models;

namespace UsrAccount.Data
{ 
    public static class DbInitializer
    {
        public static void Initialize(AccountContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.UserIdentities.Any())
            {
                return;   // DB has been seeded
            }

            var userIdentities = new UserIdentity[]
            {
                new UserIdentity{ UserId = "rpMgr", NicName="레드핀 메니져", Password="00000", UserGroup="RedPin", Role="Manager" },
                new UserIdentity{ UserId = "fgMgr", NicName="가족팀 메니져", Password="00000", UserGroup="Family", Role="Manager" },
                new UserIdentity{ UserId = "Admin", NicName="총 관리자", Password="00000", UserGroup="All", Role="Admin", InActivity=false},
                new UserIdentity{ UserId = "rpUsr", NicName="레드핀", Password="00000", UserGroup="RedPin", Role="General", InActivity=false},
                new UserIdentity{ UserId = "fgUsr", NicName="가족팀", Password="00000", UserGroup="Family", Role="General", InActivity=false},


            };
            
            foreach (UserIdentity i in userIdentities)
            {
                context.UserIdentities.Add(i);
            }
            context.SaveChanges();

            
        }



    }
}
