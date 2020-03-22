using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LAB03_ED2.Class;

namespace LAB03_ED2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public Stack<FileCompress> lifo = new Stack<FileCompress>();
        public Huffman hf = new Huffman();
        public LZW lzw = new LZW();

        // localhost:51626/weatherforecast/compress/?Path_File=""/?Algorithm=""/
        [HttpGet("compress", Name = "GetFile")]
        public IActionResult Get(string Algorithm, string Path_File)
        {
            if (Path_File == null) //no var
                return null;
            else if (Path_File.Contains(".txt"))
            {
                //what method the compress?
                if(Algorithm == "Huffman")
                {
                    string Name_new = "";
                    char[] path = Path_File.ToArray();
                    int cont = path.Length - 1; //last position
                    path[cont - 2] = '+'; //delete 'txt' 
                    for (int i = 0; i < path.Length; i++)
                    {
                        if (path[i] != '+' && path[i - 1] == '.')
                        {
                            Name_new = Name_new + path[i];
                        }
                        else
                        {
                            Name_new = Name_new + "huff";
                            i = path.Length;
                        }
                    }
                    //completed stack
                    string metrics = "";
                    hf.Compress(Path_File, Name_new);
                    metrics = hf.GetFilesMetrics("file", Path_File, Name_new);
                    string[] metrics_total = metrics.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    float rc = Convert.ToInt32(metrics_total[1]);
                    float fc = Convert.ToInt32(metrics_total[2]);
                    float rp = Convert.ToInt32(metrics_total[3]);
                    string algorithm = "Huffman";
                    FileCompress n_compress = new FileCompress(Path_File, Name_new, rc, fc, rp, algorithm);
                    lifo.Push(n_compress);
                    return null;
                }
                else if (Algorithm == "LZW")
                {
                    string Name_new = "";
                    char[] path = Path_File.ToArray();
                    int cont = path.Length - 1; //last position
                    path[cont - 2] = '+'; //delete 'txt' 
                    for (int i = 0; i < path.Length; i++)
                    {
                        if (path[i] != '+' && path[i - 1] == '.')
                        {
                            Name_new = Name_new + path[i];
                        }
                        else
                        {
                            Name_new = Name_new + "lzw";
                            i = path.Length;
                        }
                    }
                    //completed stack
                    string metrics = "";
                    lzw.Compress(Path_File, Name_new);
                    metrics = lzw.GetFilesMetrics("file", Path_File, Name_new);
                    string[] metrics_total = metrics.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    float rc = Convert.ToInt32(metrics_total[1]);
                    float fc = Convert.ToInt32(metrics_total[2]);
                    float rp = Convert.ToInt32(metrics_total[3]);
                    string algorithm = "LZW";
                    FileCompress n_compress = new FileCompress(Path_File, Name_new, rc, fc, rp, algorithm);
                    lifo.Push(n_compress);
                    return null;
                }
                else  //warning: method wrong
                {       
                    return null;
                }
            }
            else //no contain a text file
            {
                return null;
            }
        }

        // localhost:51626/weatherforecast/decompress/?Algorithm2=""/?Path_File=""
        [HttpGet("decompress", Name = "GetFile2")]
        public IActionResult Get1(string Algorithm2, string Path_File2)
        {
            if (Algorithm2 == "Huffman")
            {
                if (Path_File2.Contains(".huff"))
                {
                    string decompress = "";
                    decompress = hf.uncompress(Path_File2);
                    return null;
                }
                else
                    return null;
            }
            else if (Algorithm2 == "LZW")
            {
                if (Path_File2.Contains(".lzw"))
                {
                    lzw.Uncompress(Path_File2,Path_File2);
                    return null;
                }
                else
                    return null;
            }
            else
                return null;
           
        }

        // localhost:51626/weatherforecast/compressions""
        [HttpGet("compressions")]
        public IEnumerable<FileCompress> Get2()
        {
                return lifo; //return stack
        }

    }
}
