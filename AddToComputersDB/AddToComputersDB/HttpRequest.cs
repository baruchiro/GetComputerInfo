using AddToComputersDB.Models;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddToComputersDB
{
    class HttpRequest
    {
        private static string ExecuteRequest(HttpWebRequest httpRequest)
        {
            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponseAsync().Result;  //this is Task<>
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            return streamReader.ReadToEnd();
        }

        private static string GET(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            return ExecuteRequest(request);
        }

        private static string POST(string url, string payload)
        {
            payload = "data=" + payload;
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";


            StreamWriter streamWriter = new StreamWriter(request.GetRequestStreamAsync().Result);
            streamWriter.Write(payload);
            streamWriter.Flush();

            return ExecuteRequest(request);
        }

        public static Processors GetProcessorDetailsFromCPUBenchmark(string cpuName)
        {
            Processors proc = new Processors();
            cpuName = cpuName.Replace("+", "%2B");
            cpuName = cpuName.Replace(" ", "+");

            string result = GET("https://www.cpubenchmark.net/cpu.php?cpu=" + cpuName);

            Regex rankRgx = new Regex("<span style=\"font-family: Arial, Helvetica, sans-serif;font-size: 35px;	font-weight: bold; color: red;\">([^[0-9]+$])</span>");
            Match match = rankRgx.Match(result);
            if (match.Success)
            {
                proc.rank =Convert.ToInt32( match.Groups[1].Value);
            }

            rankRgx = new Regex("<strong>Socket:</strong>\"([*])\"<br>");
            match = rankRgx.Match(result);

            return null;
        }
    }
}