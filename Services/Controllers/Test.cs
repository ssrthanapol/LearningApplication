using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningWeb.Models;

namespace Services.Controllers
{
    class Test
    {
        public void TempData()
        {
            var context = new Entities();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("start");
                    for (int i = 0; i < 10; i++)
                    {
                        TOP top = new TOP();
                        top.ID = i;
                        context.TOPs.Add(top);
                        for (int j = 0; j < 10; j++)
                        {
                            MID mid = new MID();
                            mid.ID = j;
                            mid.TOP_ID = i;
                            context.MIDs.Add(mid);
                            for (int k = 0; k < 10; k++)
                            {
                                BOT bot = new BOT();
                                bot.ID = k;
                                bot.MID_ID = j;
                                bot.TOP_ID = i;
                                context.BOTs.Add(bot);
                                //context.SaveChanges();
                            }
                        }
                    }
                    context.SaveChanges();
                    tran.Commit();
                    Console.WriteLine("complete");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }
            }
        }

        public void TestFunction1()
        {
            var context = new Entities();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("start");
                    var toplist = context.TOPs.ToList();
                    foreach(var t in toplist)
                    {
                        TOP_TEST top = new TOP_TEST();
                        //top.ID = t.ID;
                        context.TOP_TEST.Add(top);

                        var midlist = context.MIDs.Where(w => w.TOP_ID == t.ID).ToList();
                        foreach (var m in midlist)
                        {
                            MID_TEST mid = new MID_TEST();
                            //mid.ID = m.ID;
                            mid.TOP_ID = top.ID; //can ?
                            context.MID_TEST.Add(mid);

                            var botlist = context.BOTs.Where(w => w.MID_ID == m.ID).ToList();
                            foreach (var b in botlist)
                            {
                                BOT_TEST bot = new BOT_TEST();
                                bot.ID = b.ID;
                                bot.MID_ID = mid.ID; //can ?
                                bot.TOP_ID = top.ID; //can ?
                                context.BOT_TEST.Add(bot);
                                context.SaveChanges();
                            }
                        }
                    }
                    context.SaveChanges();
                    tran.Commit();
                    Console.WriteLine("complete");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }
            }
        }

        public async Task TestFunction2()
        {
            using (var context = new Entities().Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("");
                    context.Commit();
                }
                catch (Exception ex)
                {
                    context.Rollback();
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }
            }
        }
    }
}
