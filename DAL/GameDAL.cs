using Entity_new;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GameDAL
    {
        public void AddOrUpdate(Entity_new.GameEnity auth)
        {

            using (bakaContext data = new bakaContext())
            {
                var t = data.Games.FirstOrDefault(item => item.Id == auth.Id);
                if (t == null)
                {

                    data.Games.Add(new Game()
                    {
                        Host = auth.Host,
                        Active = auth.Active
                    });
                }
                else
                {
                    t.Host = auth.Host;
                    t.Active = auth.Active;
                }
                data.SaveChanges();
            }
        }

        public Entity_new.GameEnity Get(int id)
        {
            using (bakaContext data = new bakaContext())
            {
                var t = data.Games.FirstOrDefault(item => id == item.Id);
                if (t == null)
                    return null;
                GameEnity en = new GameEnity();
                en.Id = t.Id;
                en.Host = t.Host;
                en.Active = t.Active;

                return en;
            }

        }
    }
}
