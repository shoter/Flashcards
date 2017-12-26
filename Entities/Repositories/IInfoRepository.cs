using Flashcards.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IInfoRepository
    {
        UserInfo GetUserInfo(string userID);
    }
}
