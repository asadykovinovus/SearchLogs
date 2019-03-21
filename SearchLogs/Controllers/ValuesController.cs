using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SearchLogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values/5
        [HttpGet("{pattern}")]
        public ActionResult<string> GetLogs(string pattern)
        {
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json")
                                    .Build();
            string Path = configuration["FilePath"];
            String returnString;
            Regex regex = new Regex(pattern);
            using (StreamReader sr = new StreamReader(Path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (regex.Matches(line).Count > 0)
                    {
                        returnString = line + "\n";
                        for (int i = 1; i < 20; i++)
                        {
                            returnString += sr.ReadLine() + "\n";
                        }
                        return returnString;
                    }

                }
            }
            return "Искомое значение не найдено " + pattern + " " + Path;
        }
    }
}
