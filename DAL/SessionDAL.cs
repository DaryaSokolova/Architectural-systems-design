using Entity_new;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SessionDAL
    {
        public void AddOrUpdate(Entity_new.SessionEnity auth)
        {

            using (bakaContext data = new bakaContext())
            {
                var t = data.Sessions.FirstOrDefault(item => item.GameId == auth.GameId);
                if (t == null)
                {

                    data.Sessions.Add(new Session()
                    {
                        Id = t.Id,
                        UserId = t.UserId,
                        GameId = t.GameId
                    });
                }
                else
                {
                    t.Id = auth.Id;
                    t.UserId = auth.UserId;
                    t.GameId = auth.GameId;
                }
                data.SaveChanges();
            }
        }

        public Entity_new.SessionEnity Get(int id)
        {
            using (bakaContext data = new bakaContext())
            {
                var t = data.Sessions.FirstOrDefault(item => id == item.Id);
                if (t == null)
                    return null;
                SessionEnity en = new SessionEnity();
                en.Id = t.Id;
                en.UserId = t.UserId;
                en.GameId = t.GameId;

                return en;
            }

        }
    }
}
