using MahaWikiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MahaWikiAPI.Controllers
{
    public class MHController : ApiController
    {
        MahaWikiEntities1 db = new MahaWikiEntities1();
        

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("getContents")]

        public Dictionary<string,string> getContentsUNCKey(string ukey)
        {

            Dictionary<string, string> retDict = new Dictionary<string, string>();
            content cont = null;
            try
            {
                cont = db.contents.ToList().Where( (c) => { return c.uncKey == ukey; }).FirstOrDefault();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (cont == null){

                retDict.Add("Error", "No record found");
                return retDict;
                //return Ok("No contents found for the search word");
            }
            else
            {
                
                retDict.Add("description", cont.description);
                retDict.Add ("category" , cont.category);
                retDict.Add("notes", cont.notes);
                retDict.Add("usage Region", cont.usageRegion);
                retDict.Add("aka", cont.AKA);
                retDict.Add("uncKey", cont.uncKey);
                retDict.Add("ansiKey", cont.ansiKey);
                // var json = Newtonsoft.Json.JsonConvert.SerializeObject(cont);
                //return Json(json);
                return retDict;
            }
        }
        public Dictionary<string, string> getContentsANSIKey(string akey)
        {
            Dictionary<string, string> retDict = new Dictionary<string, string>();
            content cont = null;
            try
            {
                cont = db.contents.ToList().Where((c) => { return c.ansiKey == akey; }).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (cont == null)            {

                retDict.Add("Error", "No record found");
                return retDict;
            }
            else
            {
                retDict.Add("description", cont.description);
                retDict.Add("category", cont.category);
                retDict.Add("notes", cont.notes);
                retDict.Add("usage Region", cont.usageRegion);
                retDict.Add("aka", cont.AKA);
                retDict.Add("uncKey", cont.uncKey);
                retDict.Add("ansiKey", cont.ansiKey);
                // var json = Newtonsoft.Json.JsonConvert.SerializeObject(cont);
                //return Json(json);
                return retDict;
            }
        }
        public List<string> getContentsCategory(string catkey)
        {
            List<string> uncList = new List<string>();
            try
            {
                string sqlQuery = "SELECT uncKey FROM contents where category = N'" + catkey + "'";
                using (db)
                {
                    uncList = db.Database.SqlQuery<string>(sqlQuery).ToList();
                    //contentList = db.contents.SqlQuery("Select uncKey from contents where category = '" +catkey +"'").ToList<content>();                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return uncList;
        }
        public List<string> getDistinctCategories()
        {
            List<string> catList = new List<string>();
            content cont = null;
            try
            {
                // results = db.contents.Select(m => m.category).Distinct();
                var results = (from con in db.contents select con.category).Distinct();
                catList = results.ToList<string>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return catList;
        }

    }
}
